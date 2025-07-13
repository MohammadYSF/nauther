import React from 'react';
import { ConfigProvider } from 'antd';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import AdminPage from './pages/AdminPage';
import AdminNewPage from './pages/AdminNewPage';
import RolePage from './pages/RolePage';
import RoleNewPage from './pages/RoleNewPage';
import PermissionPage from './pages/PermissionPage';
import Layout from './components/Layout';
import { OidcProvider } from '@axa-fr/react-oidc';
import Login from './pages/LoginPage';
import NotFoundPage from './pages/NotFoundPage';
import { themeConfig } from './theme/themeConfig';
import { AuthenticatingComponent, AuthenticatingErrorComponent, CallbackSuccessComponent, LoadingComponent } from './components/OidcComponents';
import { SignOutSuccessPage } from './pages/SignOutCallbackPage';
import { ssoServerConfig } from './config/ssoServerConfig';
import { antdLocale } from './theme/locale';
import { PrivateRoute } from './components/PrivateRoute';


const App: React.FC = () => {
  return (
    <ConfigProvider
      theme={themeConfig}
      direction="rtl"
      locale={antdLocale}
    >
      <BrowserRouter>
        <OidcProvider
         loadingComponent={LoadingComponent}
          authenticatingComponent={AuthenticatingComponent}
         authenticatingErrorComponent={AuthenticatingErrorComponent}
         callbackSuccessComponent={CallbackSuccessComponent}         
          configuration={ssoServerConfig}
         
         >
          {/* <AuthProvider> */}
          <Routes>
            <Route path="/signout-callback-oidc" element={<SignOutSuccessPage />} />

            <Route path="/login" element={<Login />} />
            {/* <Route path="/signin-oidc" element={<SignInOidc />} /> */}
            <Route element={<Layout />} >
              {/* <Route element={<PrivateRoute />}> */}
                <Route path="/" element={<AdminPage />} />
                <Route path="/permission"
                  element={<PermissionPage />}
                />
                <Route path="/role" element={<RolePage />} />
                <Route path="/role/new" element={<RoleNewPage />} />
                <Route path="/role/edit/:id" element={<RoleNewPage />} />
                <Route path="/admin/new" element={<AdminNewPage />} />
                <Route path="/admin/edit/:id" element={<AdminNewPage />} />
              {/* </Route> */}
            </Route>
            <Route path="*" element={<NotFoundPage />} />
          </Routes>
          {/* </AuthProvider> */}
        </OidcProvider>
      </BrowserRouter>
    </ConfigProvider>
  );
};

export default App;

