import { Typography, Input, Button, Select, Card, Form } from 'antd';
import Sidebar from '../components/Sidebar';
import Topbar from '../components/Topbar';
import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getRoleById, getRoles } from '../services/roleService';
import { getPermissions, type GetPermissionsResponseDataModel } from '../services/permissionService';

export default function RoleNewPage() {
  const { id } = useParams();
  const isEdit = Boolean(id);

  const [roles, setRoles] = useState<any[]>([]);
  const [permissions, setPermissions] = useState<GetPermissionsResponseDataModel>({ data: [], total: 0 });
  const [roleName, setRoleName] = useState('');
  const [displayName, setDisplayName] = useState('');
  const [selectedPermissions, setSelectedPermissions] = useState<string[]>([]);

  useEffect(() => {
    getRoles().then(res => setRoles(res.data || []));
    getPermissions().then(res => setPermissions(res));
  }, []);

  useEffect(() => {
    if (isEdit && id) {
      getRoleById(id).then(res => {
        setRoleName(res.name);
        setSelectedPermissions(res.permissions.map((p: any) => p.id));
      });
    }
  }, [id, isEdit]);

  return (
    <div style={{ background: '#f4f6f8', minHeight: '100vh', width: '100vw', padding: 0, margin: 0 }}>
      <div style={{ width: '100vw', minHeight: '100vh', position: 'relative' }}>
        <Sidebar />
        <div style={{ paddingRight: 56 }}>
          <Topbar />
          <Card style={{ padding: 32, maxWidth: 900, margin: '40px auto', borderRadius: 16 }}>
            <Typography.Title level={5} style={{ fontWeight: 700, marginBottom: 24 }}>
              <span style={{ color: '#337ab7', fontWeight: 700 }}>
                {isEdit ? `ویرایش نقش ${id}` : 'نقش جدید'}
              </span>
              <span style={{ color: '#bdbdbd', fontWeight: 400, fontSize: 22, marginRight: 8 }}>{' > '}</span>
            </Typography.Title>
            <Form layout="vertical" style={{ maxWidth: 600, margin: '0 auto', textAlign: 'right' }}>
              <Form.Item label="نام نقش">
                <Input
                  value={roleName}
                  onChange={e => setRoleName(e.target.value)}
                  placeholder="نام نقش را وارد کنید"
                />
              </Form.Item>
              <Form.Item label="نام نمایشی نقش">
                <Input
                  value={displayName}
                  onChange={e => setDisplayName(e.target.value)}
                  placeholder="نام نمایشی را وارد کنید"
                />
              </Form.Item>
              <Form.Item label="دسترسی‌ها">
                <Select
                  mode="multiple"
                  value={selectedPermissions}
                  onChange={setSelectedPermissions}
                  placeholder="انتخاب کنید"
                  style={{ width: '100%' }}
                  optionLabelProp="label"
                >
                  {permissions.data.map((perm, idx) => (
                    <Select.Option key={perm.id} value={perm.id} label={perm.displayName}>
                      {perm.name}
                    </Select.Option>
                  ))}
                </Select>
              </Form.Item>
              <Form.Item>
                <Button type="primary" style={{ minWidth: 100, borderRadius: 8 }}>
                  ذخیره
                </Button>
              </Form.Item>
            </Form>
          </Card>
        </div>
      </div>
    </div>
  );
}