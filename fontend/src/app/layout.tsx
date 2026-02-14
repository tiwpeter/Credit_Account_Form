import type { Metadata } from 'next';
import './globals.css';

export const metadata: Metadata = {
  title: 'Credit Application System',
  description: 'Complete credit application system for banks',
  icons: {
    icon: '/favicon.ico',
  },
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body className="bg-gray-50 text-gray-900">
        <div className="min-h-screen">
          {/* Navigation */}
          <nav className="bg-white border-b border-gray-200 shadow-sm sticky top-0 z-50">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
              <div className="flex justify-between items-center h-16">
                <div className="flex items-center gap-2">
                  <div className="w-8 h-8 rounded-lg bg-blue-600" />
                  <h1 className="text-xl font-bold text-gray-900">CreditApp</h1>
                </div>
                <div className="flex gap-6">
                  <a href="/applications" className="text-gray-600 hover:text-gray-900">
                    Applications
                  </a>
                  <a href="/" className="text-gray-600 hover:text-gray-900">
                    Home
                  </a>
                </div>
              </div>
            </div>
          </nav>

          {/* Main Content */}
          <main className="min-h-[calc(100vh-64px)]">
            {children}
          </main>

          {/* Footer */}
          <footer className="bg-gray-900 text-white mt-16">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
              <div className="grid grid-cols-4 gap-8 mb-8">
                <div>
                  <h3 className="font-semibold mb-4">Product</h3>
                  <ul className="space-y-2 text-sm text-gray-400">
                    <li><a href="#" className="hover:text-white">Features</a></li>
                    <li><a href="#" className="hover:text-white">Pricing</a></li>
                    <li><a href="#" className="hover:text-white">Security</a></li>
                  </ul>
                </div>
                <div>
                  <h3 className="font-semibold mb-4">Company</h3>
                  <ul className="space-y-2 text-sm text-gray-400">
                    <li><a href="#" className="hover:text-white">About</a></li>
                    <li><a href="#" className="hover:text-white">Blog</a></li>
                    <li><a href="#" className="hover:text-white">Careers</a></li>
                  </ul>
                </div>
                <div>
                  <h3 className="font-semibold mb-4">Legal</h3>
                  <ul className="space-y-2 text-sm text-gray-400">
                    <li><a href="#" className="hover:text-white">Privacy</a></li>
                    <li><a href="#" className="hover:text-white">Terms</a></li>
                    <li><a href="#" className="hover:text-white">Cookies</a></li>
                  </ul>
                </div>
                <div>
                  <h3 className="font-semibold mb-4">Contact</h3>
                  <ul className="space-y-2 text-sm text-gray-400">
                    <li><a href="#" className="hover:text-white">Support</a></li>
                    <li><a href="#" className="hover:text-white">Email</a></li>
                    <li><a href="#" className="hover:text-white">Phone</a></li>
                  </ul>
                </div>
              </div>
              <div className="border-t border-gray-800 pt-8 text-center text-sm text-gray-400">
                <p>&copy; 2026 Credit Application System. All rights reserved.</p>
              </div>
            </div>
          </footer>
        </div>
      </body>
    </html>
  );
}
