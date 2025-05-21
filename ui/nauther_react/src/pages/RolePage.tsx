import { Button, Typography, Card } from 'antd';
import { useNavigate } from 'react-router-dom';
import RoleTable from '../components/RoleTable';
import { useEffect, useState } from 'react';
import { getRoles } from '../services/roleService';

export default function RolePage() {
  const navigate = useNavigate();
  const [roles, setRoles] = useState([] as any[]);
  const [selected, setSelected] = useState<string[]>([]);
  const handleDeleteSelected = () => {
    setSelected([]);
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
              overflow: 'auto',
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
              <Button
                style={{ marginRight: 8 }}
                type="primary"
                danger
                disabled={selected.length === 0}
                onClick={handleDeleteSelected}
              >
                حذف انتخاب شده‌ها
              </Button>
            </div>
            <RoleTable />
          </Card>
  );
}