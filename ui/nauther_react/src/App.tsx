import { ConfigProvider } from 'antd';
import { BrowserRouter, Routes, Route, useLocation, Navigate, Outlet } from 'react-router-dom';
import AdminPage from './pages/AdminPage';
import AdminNewPage from './pages/AdminNewPage';
import RolePage from './pages/RolePage';
import RoleNewPage from './pages/RoleNewPage';
import PermissionPage from './pages/PermissionPage';
import Layout from './components/Layout';
import { OidcProvider, OidcSecure } from '@axa-fr/react-oidc';
import Login from './pages/LoginPage';
import { AuthProvider, useAuth } from './contexts/AuthContext';

// This configuration use hybrid mode
// ServiceWorker are used if available (more secure) else tokens are given to the client
// You need to give inside your code the "access_token" when using fetch
const configuration = {
  client_id: 'interactive.public.short',
  redirect_uri: window.location.origin + '/authentication/callback',
  silent_redirect_uri: window.location.origin + '/authentication/silent-callback',
  scope: 'openid profile email api offline_access', // offline_access scope allow your client to retrieve the refresh_token
  authority: 'https://demo.duendesoftware.com',
  service_worker_relative_url: '/OidcServiceWorker.js', // just comment that line to disable service worker mode
  service_worker_only: false,
  demonstrating_proof_of_possession: false,
};


function App() {
  return (
    <OidcProvider configuration={configuration}>

      <ConfigProvider direction="rtl">
        <BrowserRouter>
          <AuthProvider>
            <Routes>
              <Route path="/login" element={<Login />} />
              <Route element={<Layout />} >


                <Route element={<PrivateRoute />}>
                  <Route path="/" element={<AdminPage />} />
                  <Route path="/permission"
                    element={<PermissionPage />}
                  />
                  <Route path="/role" element={<RolePage />} />
                  <Route path="/role/new" element={<RoleNewPage />} />
                  <Route path="/role/edit/:id" element={<RoleNewPage />} />
                  <Route path="/admin/new" element={<AdminNewPage />} />
                  <Route path="/admin/edit/:id" element={<AdminNewPage />} />
                </Route>
              </Route>
            </Routes>
          </AuthProvider>
        </BrowserRouter>
      </ConfigProvider >
    </OidcProvider>
  );
}

export default App;

const PrivateRoute: React.FC = () => {
  const { accessToken } = useAuth();
  const location = useLocation();

  if (!accessToken) {
    // Redirect to login, preserving the current location
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return <Outlet />;
};

export { PrivateRoute };