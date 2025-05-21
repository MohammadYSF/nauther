import { Tooltip, Button } from 'antd';
import {
  UserOutlined,
  TeamOutlined,
  SafetyCertificateOutlined
} from '@ant-design/icons';
import { Link } from 'react-router-dom';

const menu = [
  { icon: <SafetyCertificateOutlined />, label: 'دسترسی', route: '/permission' },
  { icon: <TeamOutlined />, label: 'نقش', route: '/role' },
  { icon: <UserOutlined />, label: 'ادمین', route: '/' },
];

export default function Sidebar() {
  return (
    <div
      style={{
        width: 56,
        background: '#337ab7',
        height: '100%',
        position: 'absolute',
        right: 0,
        top: 0,
        borderRadius: '0 0 8px 0',
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        paddingTop: 88,
        zIndex: 2,
      }}
    >
      {menu.map((item, idx) => (
        <Tooltip title={item.label} placement="left" key={idx}>
          <Link to={item.route} style={{ display: 'block' }}>
            <Button type="text" shape="circle" style={{ color: '#fff', marginBottom: 16, fontSize: 20 }}>
              {item.icon}
            </Button>
          </Link>
        </Tooltip>
      ))}
    </div>
  );
}