import type { Config } from 'tailwindcss'

const config: Config = {
  content: [
    './src/pages/**/*.{js,ts,jsx,tsx,mdx}',
    './src/components/**/*.{js,ts,jsx,tsx,mdx}',
    './src/app/**/*.{js,ts,jsx,tsx,mdx}',
  ],
  theme: {
    extend: {
      colors: {
        navy: {
          dark: '#1E3A5F',
          medium: '#2C5282',
          light: '#3A6EA5'
        },
        gold: {
          DEFAULT: '#D4AF37',
          light: '#E8D88B',
          dark: '#B8941F'
        },
        bg: {
          light: '#F8F9FC',
          white: '#FFFFFF'
        },
        border: {
          DEFAULT: '#E1E4ED',
          light: '#F0F2F7'
        }
      },
      fontFamily: {
        sans: ['Inter', 'sans-serif'],
      },
      spacing: {
        '18': '4.5rem',
        '88': '22rem',
        '128': '32rem',
      },
      boxShadow: {
        'card': '0 2px 8px rgba(30, 58, 95, 0.08)',
        'card-hover': '0 4px 16px rgba(30, 58, 95, 0.12)',
        'input-focus': '0 0 0 3px rgba(212, 175, 55, 0.2)',
      },
      borderRadius: {
        'card': '12px',
      }
    },
  },
  plugins: [],
}

export default config
