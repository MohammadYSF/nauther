import { Outlet } from 'react-router-dom';
import Sidebar from './Sidebar';
import Topbar from './Topbar';

const Layout = () => (
  <div style={{}}>
    <div style={{ paddingRight: 46,marginTop:-8,marginLeft:-6 }}>

      <Topbar />
    </div>
    <Sidebar />
    <div style={{ paddingRight: 56 }}>
      <Outlet />

    </div>
  </div>
);

export default Layout; 