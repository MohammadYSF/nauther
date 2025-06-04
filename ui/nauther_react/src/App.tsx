import React from 'react';
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
import NotFoundPage from './pages/NotFoundPage';
import { AuthProvider, useAuth } from './contexts/AuthContext';
import { themeConfig } from './theme/themeConfig';
import SignInOidc from './pages/SignInOidc';

// This configuration use hybrid mode
// ServiceWorker are used if available (more secure) else tokens are given to the client
// You need to give inside your code the "access_token" when using fetch
const configuration = {
  client_id: 'skoruba_identity_admin',
  redirect_uri: window.location.origin + '/signin-oidc',
  // silent_redirect_uri: window.location.origin + '/signin-oidc',
  scope: 'openid profile email', // offline_access scope allow your client to retrieve the refresh_token
  authority: 'https://localhost:44310',
  // service_worker_relative_url: '/OidcServiceWorker.js', // just comment that line to disable service worker mode
  service_worker_only: false,
  demonstrating_proof_of_possession: false,
  response_type: 'code', // triggers PKCE
  token_request_extras: {
    client_secret: "skoruba_admin_client_secret",
  },
  extras:{
    client_secret: "skoruba_admin_client_secret",
  }

};

const App: React.FC = () => {
  return (
    <ConfigProvider
      theme={themeConfig}
      direction="rtl"
      locale={{
        locale: 'fa_IR',
        Pagination: {
          items_per_page: 'در صفحه',
          jump_to: 'رفتن به',
          jump_to_confirm: 'تایید',
          page: 'صفحه',
          prev_page: 'صفحه بعدی',
          next_page: 'صفحه قبلی',
          prev_5: '۵ صفحه بعدی',
          next_5: '۵ صفحه قبلی',
          prev_3: '۳ صفحه بعدی',
          next_3: '۳ صفحه قبلی'
        },
        Table: {
          emptyText: 'داده‌ای وجود ندارد.',
          filterTitle: 'فیلتر',
          filterConfirm: 'تایید',
          filterReset: 'پاک کردن',
          selectAll: 'انتخاب همه',
          selectInvert: 'معکوس کردن انتخاب',
          selectionAll: 'انتخاب همه',
          sortTitle: 'مرتب‌سازی',
          expand: 'نمایش جزئیات',
          collapse: 'بستن جزئیات'
        },
        Select: {
          notFoundContent: 'موردی یافت نشد'
        },
        DatePicker: {
          lang: {
            locale: 'fa_IR',
            today: 'امروز',
            now: 'اکنون',
            backToToday: 'بازگشت به امروز',
            ok: 'تایید',
            clear: 'پاک کردن',
            month: 'ماه',
            year: 'سال',
            week: 'هفته',
            timeSelect: 'انتخاب زمان',
            dateSelect: 'انتخاب تاریخ',
            monthSelect: 'انتخاب ماه',
            yearSelect: 'انتخاب سال',
            decadeSelect: 'انتخاب دهه',
            yearFormat: 'YYYY',
            dateFormat: 'YYYY/MM/DD',
            dayFormat: 'DD',
            dateTimeFormat: 'YYYY/MM/DD HH:mm:ss',
            monthFormat: 'MMMM',
            monthBeforeYear: true,
            previousMonth: 'ماه قبل (PageUp)',
            nextMonth: 'ماه بعد (PageDown)',
            previousYear: 'سال قبل (Control + left)',
            nextYear: 'سال بعد (Control + right)',
            previousDecade: 'دهه قبل',
            nextDecade: 'دهه بعد',
            previousCentury: 'قرن قبل',
            nextCentury: 'قرن بعد',
            placeholder: 'انتخاب تاریخ'
          },
          timePickerLocale: {
            placeholder: 'انتخاب زمان'
          }
        }
      }}
    >
      <BrowserRouter>
        <OidcProvider configuration={configuration}>
          {/* <AuthProvider> */}
          <Routes>
            <Route path="/signout-callback-oidc" element={<h2>Goodbye, you magnificent user.</h2>} />

            <Route path="/login" element={<Login />} />
            {/* <Route path="/signin-oidc" element={<SignInOidc />} /> */}
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
            <Route path="*" element={<NotFoundPage />} />
          </Routes>
          {/* </AuthProvider> */}
        </OidcProvider>
      </BrowserRouter>
    </ConfigProvider>
  );
};

export default App;

const PrivateRoute: React.FC = () => {
  // const { accessToken } = useAuth();
  const location = useLocation();

  // if (!accessToken) {
  //   // Redirect to login, preserving the current location
  //   return <Navigate to="/login" state={{ from: location }} replace />;
  // }

  return (
    <OidcSecure>
      <Outlet />
    </OidcSecure>);
};

export { PrivateRoute };