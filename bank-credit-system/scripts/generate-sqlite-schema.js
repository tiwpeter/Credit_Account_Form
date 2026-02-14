const fs = require('fs');
const path = require('path');

const inputPath = path.resolve(__dirname, '../prisma/schema.prisma');
const outputPath = path.resolve(__dirname, '../prisma/schema.dev.prisma');

let src = fs.readFileSync(inputPath, 'utf8');

// Replace the datasource block entirely to use SQLite dev file
src = src.replace(/datasource\s+db\s+\{[\s\S]*?\}/m, `datasource db {\n  provider = "sqlite"\n  url      = "file:./dev.db"\n}\n`);

// Remove provider-specific native type annotations like @db.Decimal(...), @db.Date, etc.
// 1) Remove patterns with parentheses: @db.Type(...)
src = src.replace(/@db\.[A-Za-z0-9_]+\([^)]*\)/g, '');
// 2) Remove patterns without parentheses: @db.Type
src = src.replace(/@db\.[A-Za-z0-9_]+/g, '');

// Normalize only repeated spaces and tabs (preserve newlines)
src = src.replace(/[ \t]{2,}/g, ' ');

// Ensure consistent newlines
src = src.replace(/\r\n/g, '\n');

// --- Transform enums into plain String fields for SQLite dev ---
// Find all enum blocks and collect names + values
const enumRegex = /enum\s+([A-Za-z0-9_]+)\s*\{([\s\S]*?)\}/g;
let enums = {};
src = src.replace(enumRegex, (m, name, body) => {
	const values = body
		.split(/\n/)
		.map((l) => l.trim())
		.filter((l) => l && !l.startsWith('//'))
		.map((l) => l.replace(/[,]/g, '').trim())
		.filter(Boolean);
	enums[name] = values;
	// Comment out the enum block so schema remains readable
	const commented = body
		.split(/\n/)
		.map((l) => '// ' + l)
		.join('\n');
	return `// enum ${name} {\n${commented}\n// }`;
});

// Replace enum-typed fields with String
const enumNames = Object.keys(enums);
if (enumNames.length > 0) {
	const typeRegex = new RegExp(`\\b(${enumNames.join('|')})\\b`, 'g');
	src = src.replace(typeRegex, 'String');

	// Convert @default(ENUM_VALUE) into @default("ENUM_VALUE") when value is one of enum values
	src = src.replace(/@default\(([^)]+)\)/g, (m, inner) => {
		const val = inner.trim();
		// ignore function defaults like now()
		if (/^[A-Z0-9_]+$/.test(val)) {
			// if matches any enum value across enums
			for (const vals of Object.values(enums)) {
				if (vals.includes(val)) return `@default("${val}")`;
			}
		}
		return m;
	});
}

// Replace Json type with String (store JSON as string in SQLite dev)
src = src.replace(/\bJson\b/g, 'String');

// Replace primitive list types (String[], Int[], etc.) with String (store as JSON string)
src = src.replace(/\bString\[\]/g, 'String');
src = src.replace(/\bInt\[\]/g, 'String');
src = src.replace(/\bDecimal\[\]/g, 'String');

// Write output
fs.writeFileSync(outputPath, src, 'utf8');
console.log('Generated:', outputPath);

console.log('Notes:');
console.log('- datasource switched to SQLite at file:./dev.db');
console.log('- provider-native annotations (@db.*) removed for SQLite compatibility');
console.log('- Use `npm run prisma:dev` to generate client and push schema for local dev');
