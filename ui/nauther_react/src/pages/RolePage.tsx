import { Button, Typography, Card, Affix } from 'antd';
import { useNavigate } from 'react-router-dom';
import RoleTable from '../components/RoleTable';
import { useEffect, useState } from 'react';
import { getRoles, deleteRole } from '../services/roleService';

export default function RolePage() {
  const navigate = useNavigate();
  const [roles, setRoles] = useState([] as any[]);
  const [selected, setSelected] = useState<string[]>([]);
  const handleDeleteSelected = async () => {
    try {
      await Promise.all(selected.map(roleId => deleteRole(roleId)));
      setSelected([]);
      // Refresh roles after deletion
      getRoles().then(res => setRoles(res.data));
    } catch (error) {
      console.error('Error deleting roles:', error);
    }
  };
  useEffect(() => {
    getRoles().then(res => setRoles(res.data));
  }, []);

  return (
          <Card
            style={{
              border:'none',
              marginTop: 24,
              padding: 24,
              boxShadow: 'none',
              minHeight: 500,
              width: 'calc(100vw - 100px)',
              marginLeft: 'auto',
              marginRight: 'auto',
            }}
          >
              <Typography.Title level={5} style={{ fontWeight: 700 }}>نقش</Typography.Title>
            <div style={{ display: 'flex', justifyContent: 'start', alignItems: 'center', marginBottom: 16 }}>
              <Button
                type="primary"
                style={{ borderRadius: 8 }}
                onClick={() => navigate('/role/new')}
              >
                نقش جدید
              </Button>
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
            </div>
            <RoleTable
              rowSelection={{
                selectedRowKeys: selected,
                onChange: (s) => setSelected(s as string[]),
              }}
              pagination={{ pageSize: 5,current:1 }}
            />
          </Card>
  );
}