import { Button, Typography, Card } from 'antd';
import UserTable from '../components/UserTable';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';

export default function AdminPage() {
  const navigate = useNavigate();
  const [selected, setSelected] = useState<string[]>([]);
  const handleDeleteSelected = () => {
    setSelected([]);
  };
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
            <Typography.Title level={5} >ادمین</Typography.Title>
            <div style={{ display: 'flex', justifyContent: 'start', alignItems: 'center', marginBottom: 16 }}>
              <Button
                type="primary"
                style={{ borderRadius: 8 }}
                onClick={() => navigate('/admin/new')}
              >
                ادمین جدید
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
            <UserTable selected={selected} setSelected={setSelected} />
          </Card>
  );
}