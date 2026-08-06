#!/bin/bash
set -e

ROOT_DIR="$(cd "$(dirname "$0")/.." && pwd)"

echo "=============================="
echo " [1/2] บิลด์หลังบ้าน .NET (จะสร้าง/อัปเดต swagger.json อัตโนมัติ)"
echo "=============================="
dotnet build "$ROOT_DIR/backend/CreditAccountApi.csproj"

echo ""
echo "=============================="
echo " [2/2] เรียกใช้ OpenAPI Generator"
echo "=============================="
cd "$ROOT_DIR/frontend"

# ลบของเก่าก่อน generate ใหม่
rm -rf ./src/app/core/api-client

npm run codegen

echo ""
echo "===== เสร็จสิ้น: build + generate client เรียบร้อย ====="