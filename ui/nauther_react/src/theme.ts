import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  direction: 'rtl',
  palette: {
    primary: { main: '#337ab7' },
    background: { default: '#f4f6f8' },
  },
  typography: {
    fontFamily: 'Vazirmatn, Arial, sans-serif',
  },
});

export default theme;