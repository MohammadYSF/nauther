import { Typography, Input, Button, Select, Card, Form, message } from 'antd';
import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getRoleById, getRoles, createRole, updateRole } from '../services/roleService';
import { getPermissions, type GetPermissionsResponseDataModel } from '../services/permissionService';

export default function RoleNewPage() {
  const { id } = useParams();
  const isEdit = Boolean(id);
  const navigate = useNavigate();

  const [permissions, setPermissions] = useState<GetPermissionsResponseDataModel>({ data: [], metadata: { total: 0 } });
  const [roleName, setRoleName] = useState('');
  const [displayName, setDisplayName] = useState('');
  const [selectedPermissions, setSelectedPermissions] = useState<string[]>([]);
  const [form] = Form.useForm();
  const [messageApi, contextHolder] = message.useMessage();

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
        const res = await createRole({ ...roleData, permissionIds: selectedPermissions });
        if (res && res.statusCode === 201) {
          messageApi.success('نقش با موفقیت ایجاد شد');
          setTimeout(() => {
            navigate('/role');

          }, 1000);
        } else if (res && res.validationErrors) {
          const fields = Object.entries(res.validationErrors).map(([field, errors]: [string, any]) => ({
            name: field,
            errors: Array.isArray(errors) ? errors : [errors],
          }));
          form.setFields(fields);
        } else if (res && res.message) {
          message.error(res.message);
        }
      }
    } catch (error: any) {
      const res = error.data;
      if (res.validationErrors) {
        const validationErrors = res.validationErrors;
        validationErrors.forEach((error: { key: string; value: string }) => {
          form.setFields([{
            name: error.key,
            errors: [error.value],
          }]);
        });
      } else if (res.message) {
        message.error(res.message);
      } else {
        message.error('خطایی رخ داده است');
      }

    }
  };

  return (
    <>
      {contextHolder}
      <Card style={{ padding: 32, maxWidth: 900, margin: '40px auto', border: 'none' }}>
        <Typography.Title level={5} style={{ fontWeight: 700, marginBottom: 24 }}>
          <span style={{ color: '#337ab7', fontWeight: 700 }}>
            {isEdit ? `ویرایش نقش ${id}` : 'نقش جدید'}
          </span>
          <span style={{ color: '#bdbdbd', fontWeight: 400, fontSize: 22, marginRight: 8 }}>{' > '}</span>
        </Typography.Title>
        <Form layout="vertical" style={{ maxWidth: 600, margin: '0 auto', textAlign: 'right' }} onFinish={handleSubmit} form={form}>
          <Form.Item name="Name" label="نام نقش">
            <Input
              value={roleName}
              onChange={e => setRoleName(e.target.value)}
              placeholder="نام نقش را وارد کنید"
            />
          </Form.Item>
          <Form.Item name="DisplayName" label="نام نمایشی نقش">
            <Input
              value={displayName}
              onChange={e => setDisplayName(e.target.value)}
              placeholder="نام نمایشی را وارد کنید"
            />
          </Form.Item>
          <Form.Item name="PermissionIds" label="دسترسی‌ها">
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
            <Button style={{ minWidth: 100, borderRadius: 8, marginRight: 8 }} onClick={() => navigate(-1)}>
              انصراف
            </Button>
          </Form.Item>
        </Form>
      </Card>
    </>
  );
}