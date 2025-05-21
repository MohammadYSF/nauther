import { Typography, Avatar } from 'antd';

export default function Topbar() {
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
          <Typography.Text style={{ color: '#fff', fontWeight: 500, marginLeft: 8 }}>
            سهند افشردی
          </Typography.Text>
          <Avatar style={{ width: 24, height: 24 }} />
        </div>
      </div>
    </div>
  );
}