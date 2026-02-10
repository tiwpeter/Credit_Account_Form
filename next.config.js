/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  swcMinify: true,
  
  // Image optimization
  images: {
    domains: ['storage.example.com'], // Add your CDN domains
  },

  // Environment variables
  env: {
    API_BASE_URL: process.env.API_BASE_URL || 'http://localhost:3000/api',
  },

  // Experimental features
  experimental: {
    typedRoutes: true,
  },
}

module.exports = nextConfig
