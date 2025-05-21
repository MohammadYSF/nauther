import type { ThemeConfig } from 'antd';
import { theme as antdTheme } from 'antd';

const theme: ThemeConfig = {
  token: {
    colorPrimary: '#337ab7',
    borderRadius: 8,
    fontFamily: 'Vazirmatn, Arial, sans-serif',
  },
  algorithm: antdTheme.defaultAlgorithm,
  // direction: 'rtl', // Direction is set in ConfigProvider
};

export default theme; 