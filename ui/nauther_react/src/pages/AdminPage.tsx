import { Button, Typography, Card, Affix } from 'antd';
import UserTable from '../components/UserTable';
import { useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { getAllUsers } from '../services/userService';
import { deleteAdmin } from '../services/adminService';

export default function AdminPage() {
  const navigate = useNavigate();
  const [selected, setSelected] = useState<string[]>([]);
  const [refresh, setRefresh] = useState(false);
  const handleDeleteSelected = () => {
    deleteAdmin({"ids":selected}).then(res => {
      setSelected([]);
      setRefresh(prev => !prev);
    });
  };

  return (

    <Card
      style={{
        border: 'none',
        marginTop: 24,
        padding: 24,
        boxShadow: 'none',
        minHeight: 500,
        overflow: 'auto',
        width: 'calc(100vw - 100px)',
        marginLeft: 'auto',
        marginRight: 'auto',
      }}
    >
      <Typography.Title level={5} >ادمین</Typography.Title>
      <div style={{ display: 'flex', justifyContent: 'start', alignItems: 'center', marginBottom: 16 }}>
        <Button
          type="primary"
          style={{ borderRadius: 8 }}
          onClick={() => navigate('/admin/new')}
        >
          ادمین جدید
        </Button>        
      </div>
      <UserTable selected={selected} setSelected={setSelected} refresh={refresh} />

      <Affix offsetBottom={20}>
        <Button
          type="primary"
          danger
          disabled={selected.length === 0}
          onClick={handleDeleteSelected}
        >
          حذف انتخاب شده‌ها
        </Button>
      </Affix>
    </Card>
  );
}