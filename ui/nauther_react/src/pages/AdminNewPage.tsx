import { Typography, Input, Button, Select, Card, Form, Avatar, List, Popover, Row, Col, message } from 'antd';
import { useEffect, useState, useRef } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getAdminById, getAdmins, editAdmin, createAdmin } from '../services/adminService';
import { getRoles } from '../services/roleService';
import { getPermissions } from '../services/permissionService';
import { getAllExternalUsers } from '../services/userService';
import debounce from 'lodash.debounce';
import type { User } from '../types/user';
import type { Axios, AxiosError } from 'axios';
import type { BaseApiResponseModel } from '../types/baseApiResponseModel';

export default function AdminNewPage() {
  const { id } = useParams();
  const isEdit = Boolean(id);

  const [users, setUsers] = useState<User[]>([]);
  const [userSearch, setUserSearch] = useState('');
  const [userPage, setUserPage] = useState(1);
  const [userHasMore, setUserHasMore] = useState(true);
  const [userLoading, setUserLoading] = useState(false);
  const [permissions, setPermissions] = useState<any[]>([]);
  const [roles, setRoles] = useState<any[]>([]);
  const [roleSearch, setRoleSearch] = useState('');
  const [permissionSearch, setPermissionSearch] = useState('');

  const navigate = useNavigate();

  const [form] = Form.useForm();

  const [messageApi, contextHolder] = message.useMessage();

  const [changePassword, setChangePassword] = useState(false);

  // Debounced user search
  const debouncedUserSearch = useRef(
    debounce((value: string) => {
      setUserPage(1);
      fetchUsers(1, value, false);
    }, 500)
  ).current;

  // Debounced role search
  const debouncedRoleSearch = useRef(
    debounce((value: string) => {
      setRoleSearch(value);
      fetchRoles(value);
    }, 500)
  ).current;

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
      debouncedUserSearch.cancel();
      debouncedRoleSearch.cancel();
      debouncedPermissionSearch.cancel();
    };
  }, [debouncedUserSearch, debouncedRoleSearch, debouncedPermissionSearch]);

  // Fetch users with search and pagination
  const fetchUsers = async (page = 1, search = '', append = false) => {
    setUserLoading(true);
    await getAllExternalUsers({ page: page, pageSize: 20, search: search }).then((res) => {
      if (append) {
        setUsers(prev => [...prev, ...res.data]);
      } else {
        setUsers(res.data);
      }
      setUserHasMore(res.data.length === 20);

    })
      .catch((error: AxiosError) => {
        messageApi.error((error.response?.data as BaseApiResponseModel).message)
      })
      .finally(() => {
        setUserLoading(false);
      });

  };

  // Fetch roles with search
  const fetchRoles = async (search = '') => {
    await getRoles({ page: 1, pageSize: 100, search: search })
      .then((res) => {
        setRoles(res.data || []);
      })
      .catch((error: AxiosError) => {
        messageApi.error((error.response?.data as BaseApiResponseModel).message)
      })
      ;
  };

  // Fetch permissions with search
  const fetchPermissions = async (search = '') => {
    await getPermissions({ page: 1, pageSize: 100, search: search }).then((res) => {

      setPermissions(res.data || []);
    })
      .catch((error: AxiosError) => {
        messageApi.error((error.response?.data as BaseApiResponseModel).message)
      })
      ;
  };

  // Initial load
  useEffect(() => {
    fetchUsers(1, '', false);
    fetchRoles();
    fetchPermissions();
  }, []);

  // Handle search
  const handleUserSearch = (value: string) => {
    setUserSearch(value);
    debouncedUserSearch(value);
  };

  // Handle role search
  const handleRoleSearch = (value: string) => {
    debouncedRoleSearch(value);
  };

  // Handle permission search
  const handlePermissionSearch = (value: string) => {
    debouncedPermissionSearch(value);
  };

  // Handle infinite scroll
  const handleUserScroll = (e: React.UIEvent<HTMLDivElement>) => {
    const target = e.target as HTMLDivElement;
    if (userHasMore && !userLoading && target.scrollTop + target.offsetHeight >= target.scrollHeight - 10) {
      const nextPage = userPage + 1;
      setUserPage(nextPage);
      fetchUsers(nextPage, userSearch, true);
    }
  };

  // Handle user selection change
  const handleUserChange = (value: string | null) => {
    form.setFieldValue('id', value);
    if (!value) {
      // Reset search and fetch all users when deselected
      setUserSearch('');
      fetchUsers(1, '', false);

    }
  };

  useEffect(() => {
    fetchRoles();
    fetchPermissions();
    // getRoles({ page: -1, pageSize: 10, search: '' }).then(res => setRoles(res.data || []));
    // getPermissions({ page: -1, pageSize: 10, search: '' }).then(res => setPermissions(res.data || []));
  }, []);


  const handleFinish = (values: any) => {
    let password = values["password"];
    let confirmPassword = values["confirmPassword"];
    // Validate passwords
    if (password !== confirmPassword) {
      messageApi.error("رمزعبورهای واردشده مطابقت ندارند");
      return;
    }

    // Prepare data
    const data = values;

    // Submit data
    if (isEdit && id) {
      // Update existing admin
      editAdmin(id, {...data,id:id}).then((response: any) => {
        messageApi.error(response.message);
        setTimeout(() => {
          navigate('/');
        }, 1000);
      }).catch((error: AxiosError) => {
        messageApi.error((error.response?.data as BaseApiResponseModel).message);
        const fields = Object.entries((error.response?.data as BaseApiResponseModel).validationErrors).map(([field, errors]: [string, any]) => ({
          name: field,
          errors: Array.isArray(errors) ? errors : [errors],
        }));
        form.setFields(fields);
      });
    } else if (!isEdit) {
      // Create new admin
      createAdmin(data).then((response: any) => {
        messageApi.error(response.message);
        setTimeout(() => {
          navigate('/');
        }, 1000);

      }).catch((error: AxiosError) => {
        messageApi.error((error.response?.data as BaseApiResponseModel).message);
        const fields = Object.entries((error.response?.data as BaseApiResponseModel).validationErrors).map(([field, errors]: [string, any]) => ({
          name: field,
          errors: Array.isArray(errors) ? errors : [errors],
        }));
        form.setFields(fields);
      });
    }
  }

  useEffect(() => {
    if (isEdit && id) {
      getAdminById(id).then(res => {
        form.setFieldsValue({
          ...res.data,
          permissions: res.data.permissions ? res.data.permissions.map((p: any) => p.id) : [],
          roles: res.data.roles ? res.data.roles.map((p: any) => p.id) : [],
          id: res.data.id
        });
      }).catch((error: AxiosError) => {
        messageApi.error((error.response?.data as BaseApiResponseModel).message);
      });
    }
  }, [id, isEdit]);

  return (
    <>
      {contextHolder}
      <Card style={{ padding: 32, maxWidth: 900, margin: '40px auto', border: 'none' }}>
        <Typography.Title level={5} style={{ fontWeight: 700, marginBottom: 24 }}>
          <span style={{ color: '#337ab7', fontWeight: 700 }}>
            {isEdit ? `ویرایش ادمین ${users.find(a => a.id == id)?.username}` : 'ادمین جدید'}
          </span>
          <span style={{ color: '#bdbdbd', fontWeight: 400, fontSize: 22, marginRight: 8 }}>{' > '}</span>
        </Typography.Title>
        <Form form={form} onFinish={handleFinish} layout="vertical" style={{ maxWidth: 600, margin: '0 auto', textAlign: 'right' }}>
          <Form.Item label="نام کاربر" name="id">
            <div style={{ position: 'relative' }}>
              <input
                type="text"
                style={{ position: 'absolute', opacity: 0, height: 0, width: 0 }}
                autoComplete="off"
              />
              <Select
                showSearch
                placeholder="انتخاب کنید"
                optionFilterProp="children"
                onSearch={handleUserSearch}
                onChange={handleUserChange}
                allowClear
                filterOption={false}
                disabled={isEdit}
                style={{ width: '100%', minWidth: 220 }}
                notFoundContent={userLoading ? <span>در حال بارگذاری...</span> : <span>داده ای یافت نشد</span>}
                onPopupScroll={handleUserScroll}
              >
                {users.map(user => (
                  <Select.Option
                    key={user.id}
                    value={user.id}
                    data-username={user.username}
                  >
                    <Avatar
                      size={24}
                      src={user.profileImage}
                      style={{ marginLeft: 8 }}
                    />
                    <span style={{ fontWeight: 500, fontSize: 15, marginLeft: 8 }}>{user.username}</span>
                    <span style={{ color: '#888', fontSize: 13 }}>{user.userCode}</span>
                  </Select.Option>
                ))}
              </Select>
            </div>
          </Form.Item>
          <Row gutter={16}>
            <Col span={12}>
              <Form.Item label="نقش‌ها" name="roles">
                <Select
                  mode="multiple"
                  placeholder="انتخاب کنید"
                  style={{ width: '100%' }}
                  optionLabelProp="label"
                  showSearch
                  onSearch={handleRoleSearch}
                  filterOption={false}
                  notFoundContent="داده‌ای یافت نشد"

                >
                  {roles.map((role, idx) => (
                    <Select.Option key={role.id} value={role.id} label={role.displayName}>
                      {role.displayName}
                    </Select.Option>
                  ))}
                </Select>
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item label="دسترسی‌ها" name="permissions">
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
                  {permissions.map((perm, idx) => (
                    <Select.Option key={perm.id} value={perm.id} label={perm.displayName}>
                      {perm.displayName}
                    </Select.Option>
                  ))}
                </Select>
              </Form.Item>
            </Col>
          </Row>
          {isEdit && !changePassword && (
            <Button
              type="dashed"
              style={{ marginBottom: 16 }}
              onClick={() => setChangePassword(true)}
              block
            >
              تغییر رمز عبور
            </Button>
          )}
          {((!isEdit) || changePassword) && (
            <>
              <Row gutter={16}>
                <Col span={12}>
                  <Form.Item label="رمز عبور جدید" name="password">
                    <Input.Password placeholder="رمز عبور جدید را وارد کنید" />
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="تکرار رمز عبور جدید" name="confirmPassword">
                    <Input.Password placeholder="تکرار رمز عبور جدید را وارد کنید" />
                  </Form.Item>
                </Col>
              </Row>
              {isEdit && (
                <Button
                  type="default"
                  style={{ marginBottom: 16 }}
                  onClick={() => {
                    setChangePassword(false);
                    form.setFieldsValue({ password: '', confirmPassword: '' });
                  }}
                  block
                >
                  انصراف از تغییر رمز عبور
                </Button>
              )}
            </>
          )}
          <Form.Item style={{ textAlign: 'center', marginTop: 24 }}>
            <Button type="default" style={{ minWidth: 100, borderRadius: 8, marginRight: 16 }} onClick={() => navigate(-1)}>
              لغو
            </Button>
            <Button htmlType='submit' type="primary" style={{ minWidth: 100, borderRadius: 8 }}>
              ذخیره
            </Button>
          </Form.Item>
        </Form>
      </Card>
    </>
  );
}