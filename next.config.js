/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  swcMinify: true,
  experimental: {
    typedRoutes: true,
  },
  eslint: {
    ignoreDuringBuilds: true,
  },
};

export default nextConfig;
