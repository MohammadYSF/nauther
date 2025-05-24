import { Typography, Input, Button, Select, Card, Form } from 'antd';
import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getRoleById, getRoles, createRole, updateRole } from '../services/roleService';
import { getPermissions, type GetPermissionsResponseDataModel } from '../services/permissionService';

export default function RoleNewPage() {
  const { id } = useParams();
  const isEdit = Boolean(id);

  const [permissions, setPermissions] = useState<GetPermissionsResponseDataModel>({ data: [], metadata: { total: 0 } });
  const [roleName, setRoleName] = useState('');
  const [displayName, setDisplayName] = useState('');
  const [selectedPermissions, setSelectedPermissions] = useState<string[]>([]);

  useEffect(() => {
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

  const handleSubmit = async () => {
    const roleData = {
      name: roleName,
      displayName: displayName,
      permissions: selectedPermissions,
    };

    try {
      if (isEdit && id) {
        // Update existing role
        await updateRole(id, { ...roleData, permissionIds: selectedPermissions });
      } else {
        await createRole({ ...roleData, permissionIds: selectedPermissions });
      }
    } catch (error) {
      console.error('Error saving role:', error);
    }
  };

  return (
          <Card style={{ padding: 32, maxWidth: 900, margin: '40px auto',border:'none' }}>
            <Typography.Title level={5} style={{ fontWeight: 700, marginBottom: 24 }}>
              <span style={{ color: '#337ab7', fontWeight: 700 }}>
                {isEdit ? `ویرایش نقش ${id}` : 'نقش جدید'}
              </span>
              <span style={{ color: '#bdbdbd', fontWeight: 400, fontSize: 22, marginRight: 8 }}>{' > '}</span>
            </Typography.Title>
            <Form layout="vertical" style={{ maxWidth: 600, margin: '0 auto', textAlign: 'right' }} onFinish={handleSubmit}>
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
                      {perm.displayName}
                    </Select.Option>
                  ))}
                </Select>
              </Form.Item>
              <Form.Item>
                <Button type="primary" style={{ minWidth: 100, borderRadius: 8 }} htmlType="submit">
                  ذخیره
                </Button>
              </Form.Item>
            </Form>
          </Card>
  );
}