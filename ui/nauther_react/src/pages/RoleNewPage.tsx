import { Typography, Input, Button, Select, Card, Form, message } from 'antd';
import { useState, useEffect, useRef } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getRoleById, createRole, updateRole } from '../services/roleService';
import { getPermissions, type GetPermissionsResponseDataModel } from '../services/permissionService';
import debounce from 'lodash.debounce';
import type { AxiosError } from 'axios';
import type { BaseApiResponseModel } from '../types/baseApiResponseModel';

export default function RoleNewPage() {
  const { id } = useParams();
  const isEdit = Boolean(id);
  const navigate = useNavigate();

  const [permissions, setPermissions] = useState<GetPermissionsResponseDataModel>({ data: [], message: "", metadata: { total: 0 }, statusCode: 200, validationErrors: [] });
  const [permissionSearch, setPermissionSearch] = useState('');
  const [form] = Form.useForm();
  const [messageApi, contextHolder] = message.useMessage();
  const [apiData, setApiData] = useState<any>(null);

  // Debounced permission search
  const debouncedPermissionSearch = useRef(
    debounce((value: string) => {
      setPermissionSearch(value);
      fetchPermissions(value);
    }, 500)
  ).current;

  // Cleanup debounce on unmount
  useEffect(() => {
    return () => {
      debouncedPermissionSearch.cancel();
    };
  }, [debouncedPermissionSearch]);

  // Fetch permissions with search
  const fetchPermissions = async (search = '') => {
    await getPermissions({ page: 1, pageSize: 100, search: search })
      .then((res) => {
        setPermissions(res);
      })
      .catch((error: AxiosError) => {
        messageApi.error((error.response?.data as BaseApiResponseModel).message);
      });
  };

  useEffect(() => {
    fetchPermissions();
  }, []);

  // Handle permission search
  const handlePermissionSearch = (value: string) => {
    debouncedPermissionSearch(value);
  };

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

  const onFinish = async (values: any) => {
    if (isEdit && id) {
      await updateRole(id, values).then((res) => {
        messageApi.success(res.message);
        setTimeout(() => {
          navigate('/role');
        }, 1000);

      }).catch((error: AxiosError) => {
        messageApi.error((error.response?.data as BaseApiResponseModel).message);
      });

    } else {
      await createRole(values).then((res) => {
        messageApi.success(res.message);
        setTimeout(() => {
          navigate('/role');
        }, 1000);
      }
      ).catch((error: AxiosError) => {
        messageApi.error((error.response?.data as BaseApiResponseModel).message);
        const fields = Object.entries((error.response?.data as BaseApiResponseModel).validationErrors).map(([field, errors]: [string, any]) => ({
          name: field,
          errors: Array.isArray(errors) ? errors : [errors],
        }));
        form.setFields(fields);
      });
    }
  }
  return (
    <>
      {contextHolder}
      <Card style={{ padding: 32, maxWidth: 900, margin: '40px auto', border: 'none' }}>
        <Typography.Title level={5} style={{ fontWeight: 700, marginBottom: 24 }}>
          <span style={{ color: '#337ab7', fontWeight: 700 }}>
            {isEdit ? `ویرایش نقش :  ${apiData?.displayName ?? ""}` : 'نقش جدید'}
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
              showSearch
              onSearch={handlePermissionSearch}
              filterOption={false}
              notFoundContent="داده‌ای یافت نشد"
            >
              {(permissions.data || []).map((perm, idx) => (
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