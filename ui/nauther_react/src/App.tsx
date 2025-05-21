import { ConfigProvider } from 'antd';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import AdminPage from './pages/AdminPage';
import AdminNewPage from './pages/AdminNewPage';
import RolePage from './pages/RolePage';
import RoleNewPage from './pages/RoleNewPage';
import PermissionPage from './pages/PermissionPage';
import Layout from './components/Layout';

function App() {
  return (
    <ConfigProvider direction="rtl">
      <BrowserRouter>
        <Routes>
          <Route element={<Layout />} >
            <Route path="/" element={<AdminPage />} />
            <Route path="/permission" element={<PermissionPage />} />
            <Route path="/role" element={<RolePage />} />
            <Route path="/role/new" element={<RoleNewPage />} />
            <Route path="/role/edit/:id" element={<RoleNewPage />} />
            <Route path="/admin/new" element={<AdminNewPage />} />
            <Route path="/admin/edit/:id" element={<AdminNewPage />} />
          </Route>
      </Routes>
    </BrowserRouter>
    </ConfigProvider >
  );
}

export default App;