import { Tooltip, Button } from 'antd';
import {
  UserOutlined,
  CameraOutlined,
  SettingOutlined,
  SearchOutlined,
  FilterOutlined,
  EllipsisOutlined,
} from '@ant-design/icons';

const menu = [
  { icon: <UserOutlined />, label: 'ادمین' },
  { icon: <CameraOutlined />, label: 'دوربین' },
  { icon: <SearchOutlined />, label: 'جستجو' },
  { icon: <FilterOutlined />, label: 'فیلتر' },
  { icon: <EllipsisOutlined />, label: 'بیشتر' },
  { icon: <SettingOutlined />, label: 'تنظیمات' },
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
        paddingTop: 48,
        zIndex: 2,
      }}
    >
      {menu.map((item, idx) => (
        <Tooltip title={item.label} placement="left" key={idx}>
          <Button type="text" shape="circle" style={{ color: '#fff', marginBottom: 16, fontSize: 20 }}>
            {item.icon}
          </Button>
        </Tooltip>
      ))}
    </div>
  );
}