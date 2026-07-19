import { defineConfig } from 'orval';

export default defineConfig({
  shopApi: {
    input: `${process.env.API_URL ?? 'http://localhost:5000'}/swagger/v1/swagger.json`,
    output: {
      mode: 'tags-split',
      target: './src/api/generated',
      schemas: './src/api/generated/model',
      indexFiles: true,
      client: 'angular',           // ← เปลี่ยนจาก react-query
      override: {
        angular: {
          provideIn: 'root',        // service ใช้ได้ทั้งแอปผ่าน DI โดยไม่ต้องประกาศใน module
        },
      },
    },
  },
});