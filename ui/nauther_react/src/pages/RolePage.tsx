import { Button, Typography, Card, Affix } from 'antd';
import { useNavigate } from 'react-router-dom';
import RoleTable from '../components/RoleTable';
import { useState, useEffect } from 'react';
import { deleteRole } from '../services/roleService';

export default function RolePage() {
  const navigate = useNavigate();
  const [selected, setSelected] = useState<string[]>([]);
  const [refresh, setRefresh] = useState(false);
  const handleDeleteSelected = async () => {
    try {
      deleteRole({"ids":selected});
      setSelected([]);
      setRefresh(prev => !prev);
    } catch (error) {
      console.error('Error deleting roles:', error);
    }
  };


  return (
    <Card style={{ padding: 32, maxWidth: 900, margin: '0px auto', border: 'none' }}>

      <Typography.Title level={5} style={{ fontWeight: 700 }}>نقش</Typography.Title>
      <div style={{ display: 'flex', justifyContent: 'start', alignItems: 'center', marginBottom: 16 }}>
        <Button
          type="primary"
          style={{ borderRadius: 8 }}
          onClick={() => navigate('/role/new')}
        >
          نقش جدید
        </Button>

      </div>
      <RoleTable
        rowSelection={{
          selectedRowKeys: selected,
          onChange: (s) => setSelected(s as string[]),
        }}
        refresh={refresh}
      />
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