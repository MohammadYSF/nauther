import { Typography, Avatar, Popover, Button, Divider } from 'antd';
import { UserOutlined } from '@ant-design/icons';
import { useState } from 'react';

export default function Topbar() {
  const [popoverOpen, setPopoverOpen] = useState(false);
  const userName = 'سهند افشردی';

  const handleLogout = () => {
    // TODO: Implement logout logic
    setPopoverOpen(false);
  };

  const popoverContent = (
    <div style={{ minWidth: 200, padding: 16, background: '#f7f8fa', borderRadius: 12, boxShadow: '0 2px 8px #00000014', textAlign: 'center' }}>
      <Avatar size={48} style={{ backgroundColor: '#7265e6', fontWeight: 700, marginBottom: 8 }} icon={<UserOutlined />} />
      <Typography.Text style={{ display: 'block', marginBottom: 4, fontWeight: 600, fontSize: 16 }}>
        {userName}
      </Typography.Text>
      <Divider style={{ margin: '12px 0' }} />
      <Button type="primary" danger block style={{ fontWeight: 500, borderRadius: 8 }} onClick={handleLogout}>
        خروج
      </Button>
    </div>
  );

  return (
    <div
      style={{
        background: '#337ab7',
        boxShadow: 'none',
        display: 'flex',
        justifyContent: 'center',
      }}
    >
      <div
        style={{
          minHeight: 56,
          padding: '12px 16px',
          display: 'flex',
          flexDirection: 'row',
          justifyContent: 'space-between',
          alignItems: 'center',
          direction: 'rtl',
          width: '100%',
        }}
      >
        {/* Right: DIMA logo */}
        <div style={{ display: 'flex', alignItems: 'center' }}>
          <Typography.Text style={{ color: '#fff', fontWeight: 700, fontSize: 18, letterSpacing: 2 }}>
            DIMA
          </Typography.Text>
        </div>
        {/* Left: User full name and avatar */}
        <div style={{ display: 'flex', alignItems: 'center' }}>
          <Popover
            content={popoverContent}
            trigger="click"
            open={popoverOpen}
            onOpenChange={setPopoverOpen}
            placement="bottomRight"
            overlayStyle={{ padding: 0 }}
          >
            <Avatar style={{ width: 32, height: 32, backgroundColor: '#7265e6', fontWeight: 700, cursor: 'pointer' }} icon={<UserOutlined />} />
          </Popover>
        </div>
      </div>
    </div>
  );
}