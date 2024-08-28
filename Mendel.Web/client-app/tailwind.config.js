/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        g: '#378e4d',
        r: '#c13030',
        b: '#4f82bc',
        e: '#60378e'
      }
    }
  },
  plugins: []
};
