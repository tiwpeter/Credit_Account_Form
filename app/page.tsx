"use client";

import Link from "next/link";

export default function Home() {
  return (
    <main>
      {/* Navigation */}
      <nav className="bg-navy-dark text-white shadow-lg">
        <div className="container mx-auto px-4 py-4 flex justify-between items-center">
          <h1 className="text-2xl font-bold">💰 Credit System</h1>
          <div className="flex gap-6">
            <Link href="/" className="hover:text-gold transition-colors">
              หน้าแรก
            </Link>
            <Link
              href="/applications"
              className="hover:text-gold transition-colors"
            >
              ใบสมัครของฉัน
            </Link>
            <Link
              href="/applications/new"
              className="px-4 py-2 bg-gold text-navy-dark rounded font-semibold hover:bg-yellow-400"
            >
              สมัครใหม่
            </Link>
          </div>
        </div>
      </nav>

      {/* Hero Section */}
      <section className="bg-gradient-to-r from-navy-dark to-blue-900 text-white py-20">
        <div className="container mx-auto px-4 text-center">
          <h2 className="text-5xl font-bold mb-6">
            ขอสินเชื่อออนไลน์อย่างง่าย
          </h2>
          <p className="text-xl mb-8 text-gray-200">
            สมัครขอสินเชื่อแค่ไม่กี่นาที
            ผ่านแบบฟอร์มสมัครออนไลน์ที่มีความปลอดภัยสูง
          </p>
          <Link
            href="/applications/new"
            className="inline-block px-8 py-4 bg-gold text-navy-dark font-bold rounded-lg hover:bg-yellow-400 text-lg"
          >
            🚀 เริ่มสมัครตอนนี้
          </Link>
        </div>
      </section>

      {/* Features */}
      <section className="py-16 bg-gray-50">
        <div className="container mx-auto px-4">
          <h3 className="text-3xl font-bold text-navy-dark mb-12 text-center">
            ทำไมต้องเลือกเรา
          </h3>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
            {[
              {
                icon: "⚡",
                title: "รวดเร็ว",
                description: "สมัครเพียง 5 ขั้นตอน ไม่เกิน 10 นาที",
              },
              {
                icon: "🔒",
                title: "ปลอดภัย",
                description:
                  "เข้ารหัส SSL และป้องกันข้อมูลส่วนบุคคลอย่างเข้มงวด",
              },
              {
                icon: "📱",
                title: "มือถือ",
                description: "สมัครได้ทั้งบนเว็บและมือถือ ที่ไหนเมื่อไหร่ก็ได้",
              },
            ].map((feature, i) => (
              <div
                key={i}
                className="bg-white rounded-lg shadow p-8 text-center hover:shadow-lg transition-shadow"
              >
                <div className="text-5xl mb-4">{feature.icon}</div>
                <h4 className="text-xl font-bold text-navy-dark mb-3">
                  {feature.title}
                </h4>
                <p className="text-gray-600">{feature.description}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Loan Types */}
      <section className="py-16">
        <div className="container mx-auto px-4">
          <h3 className="text-3xl font-bold text-navy-dark mb-12 text-center">
            ประเภทสินเชื่อ
          </h3>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-5 gap-4">
            {[
              { name: "สินเชื่อส่วนบุคคล", amount: "10K - 1M", icon: "👤" },
              { name: "สินเชื่อบ้าน", amount: "500K - 20M", icon: "🏠" },
              { name: "สินเชื่อรถยนต์", amount: "100K - 5M", icon: "🚗" },
              { name: "สินเชื่อวิสาหกิจ", amount: "100K - 10M", icon: "🏢" },
              { name: "สินเชื่อบริษัท", amount: "1M - 100M", icon: "🏛️" },
            ].map((loan, i) => (
              <div
                key={i}
                className="bg-blue-50 border border-blue-200 rounded-lg p-6 text-center hover:bg-blue-100 transition-colors"
              >
                <div className="text-4xl mb-3">{loan.icon}</div>
                <h4 className="font-bold text-navy-dark">{loan.name}</h4>
                <p className="text-sm text-gray-600 mt-2">{loan.amount}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* How it works */}
      <section className="py-16 bg-gray-50">
        <div className="container mx-auto px-4">
          <h3 className="text-3xl font-bold text-navy-dark mb-12 text-center">
            ขั้นตอนการสมัคร
          </h3>
          <div className="max-w-3xl mx-auto">
            {[
              {
                step: 1,
                title: "ข้อมูลส่วนตัว",
                desc: "กรอกข้อมูลพื้นฐานเกี่ยวกับตัวคุณ",
              },
              { step: 2, title: "ที่อยู่", desc: "ระบุที่อยู่ปัจจุบันและถาวร" },
              { step: 3, title: "รายได้", desc: "ระบุรายได้อื่นๆ การจ้างงาน" },
              {
                step: 4,
                title: "สินเชื่อ",
                desc: "เลือกประเภทและวงเงินสินเชื่อ",
              },
              { step: 5, title: "เอกสาร", desc: "อัปโหลดเอกสารประกอบ" },
              { step: 6, title: "ผู้ค้ำประกัน", desc: "ระบุผู้ค้ำประกัน" },
              {
                step: 7,
                title: "บริษัท",
                desc: "ข้อมูลบริษัท (ถ้าเป็นนิติบุคคล)",
              },
              { step: 8, title: "ตรวจสอบ", desc: "ตรวจสอบและส่งใบสมัคร" },
            ].map((item, i, arr) => (
              <div key={item.step} className="flex gap-4 mb-8">
                <div className="flex flex-col items-center">
                  <div className="w-12 h-12 bg-gold text-white rounded-full flex items-center justify-center font-bold">
                    {item.step}
                  </div>
                  {i < arr.length - 1 && (
                    <div className="w-0.5 h-12 bg-gray-300 mt-2" />
                  )}
                </div>
                <div className="pb-4">
                  <h4 className="font-bold text-navy-dark text-lg">
                    {item.title}
                  </h4>
                  <p className="text-gray-600">{item.desc}</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* CTA Section */}
      <section className="py-16 bg-navy-dark text-white">
        <div className="container mx-auto px-4 text-center">
          <h3 className="text-3xl font-bold mb-4">พร้อมที่จะเริ่มหรือยัง?</h3>
          <p className="text-xl mb-8 text-gray-200">
            สมัครขอสินเชื่อตอนนี้และรับการอนุมัติภายในไม่กี่วัน
          </p>
          <Link
            href="/applications/new"
            className="inline-block px-8 py-4 bg-gold text-navy-dark font-bold rounded-lg hover:bg-yellow-400 text-lg"
          >
            ✓ สมัครตอนนี้
          </Link>
        </div>
      </section>

      {/* Footer */}
      <footer className="bg-gray-900 text-gray-300 py-8">
        <div className="container mx-auto px-4 text-center">
          <p>&copy; 2026 Credit Application System. All rights reserved.</p>
        </div>
      </footer>
    </main>
  );
}
