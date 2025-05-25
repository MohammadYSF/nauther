import { Typography, Input, Button, Select, Card, Form, message } from 'antd';
import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getRoleById, createRole, updateRole } from '../services/roleService';
import { getPermissions, type GetPermissionsResponseDataModel } from '../services/permissionService';

export default function RoleNewPage() {
  const { id } = useParams();
  const isEdit = Boolean(id);
  const navigate = useNavigate();

  const [permissions, setPermissions] = useState<GetPermissionsResponseDataModel>({ data: [], metadata: { total: 0 } });
  const [form] = Form.useForm();
  const [messageApi, contextHolder] = message.useMessage();
  const [apiData,setApiData] = useState<any>(null);
  useEffect(() => {
    getPermissions().then(res => setPermissions(res));
  }, []);

  useEffect(() => {
    if (isEdit && id) {
      getRoleById(id).then(res => {
        setApiData(res.data);
        form.setFieldsValue({
         ...res.data,
         permissions: res.data.permissions ? res.data.permissions.map((p: any) => p.id) : [],

        });
      });
    }
  }, [id, isEdit]);

  const onFinish = async (values:any) => {
    const roleData = {
      name: values["roleName"],
      displayName: values["displayName"],
      permissionIds: values["selectedPermissions"]
    };

    try {
      if (isEdit && id) {
        // Update existing role
        await updateRole(id, { ...roleData });
      } else {
        const res = await createRole({ ...roleData });
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
            {isEdit ? `ویرایش نقش :  ${apiData?.displayName??""}` : 'نقش جدید'}
          </span>
          <span style={{ color: '#bdbdbd', fontWeight: 400, fontSize: 22, marginRight: 8 }}>{' > '}</span>
        </Typography.Title>
        <Form layout="vertical" style={{ maxWidth: 600, margin: '0 auto', textAlign: 'right' }} onFinish={onFinish} form={form}>
          <Form.Item name="name" label="نام نقش">
            <Input
              placeholder="نام نقش را وارد کنید"
            />
          </Form.Item>
          <Form.Item name="displayName" label="نام نمایشی نقش">
            <Input
              placeholder="نام نمایشی را وارد کنید"
            />
          </Form.Item>
          <Form.Item name="permissions" label="دسترسی‌ها">
            <Select
              mode="multiple"
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