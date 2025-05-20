import { ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import theme from './theme';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import AdminPage from './pages/AdminPage';
import AdminNewPage from './pages/AdminNewPage';
import RolePage from './pages/RolePage';
import RoleNewPage from './pages/RoleNewPage';
import PermissionPage from './pages/PermissionPage';

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<AdminPage />} />
          <Route path="/permission" element={<PermissionPage />} />
          <Route path="/role" element={<RolePage />} />
          <Route path="/role/new" element={<RoleNewPage />} />
          <Route path="/role/edit/:id" element={<RoleNewPage />} />
          <Route path="/admin/new" element={<AdminNewPage />} />
          <Route path="/admin/edit/:id" element={<AdminNewPage />} />
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;