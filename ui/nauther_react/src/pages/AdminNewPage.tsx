import { Typography, Input, Button, Select, Card, Form, Avatar, List, Popover, Row, Col, message } from 'antd';
import { useEffect, useState, useRef } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getAdminById, getAdmins, editAdmin, createAdmin } from '../services/adminService';
import { getRoles } from '../services/roleService';
import { getPermissions } from '../services/permissionService';
import { getAllUsers, type User } from '../services/userService';
import debounce from 'lodash.debounce';

export default function AdminNewPage() {
  const { id } = useParams();
  const isEdit = Boolean(id);

  const [users, setUsers] = useState<User[]>([]);
  const [userSearch, setUserSearch] = useState('');
  const [userPage, setUserPage] = useState(1);
  const [userHasMore, setUserHasMore] = useState(true);
  const [userLoading, setUserLoading] = useState(false);
  const [selectedUser, setSelectedUser] = useState<any>(null);
  const [permissions, setPermissions] = useState<any[]>([]);
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [roles, setRoles] = useState<any[]>([]);
  const [selectedRoles, setSelectedRoles] = useState<string[]>([]);
  const [selectedPermissions, setSelectedPermissions] = useState<string[]>([]);

  const navigate = useNavigate();

  const [form] = Form.useForm();

  // Debounced user search
  const debouncedUserSearch = useRef(
    debounce((value: string) => {
      setUserPage(1);
      fetchUsers(1, value, false);
    }, 500)
  ).current;

  // Fetch users with search and pagination
  const fetchUsers = async (page = 1, search = '', append = false) => {
    setUserLoading(true);
    const res = await getAllUsers(page, 20, search);
    if (append) {
      setUsers(prev => [...prev, ...res.data]);
    } else {
      setUsers(res.data);
    }
    setUserHasMore(res.data.length === 20);
    setUserLoading(false);
  };

  // Initial load
  useEffect(() => {
    fetchUsers(1, '', false);
  }, []);

  // Handle search
  const handleUserSearch = (value: string) => {
    setUserSearch(value);
    debouncedUserSearch(value);
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

  useEffect(() => {
    getRoles().then(res => setRoles(res.data || []));
    getPermissions().then(res => setPermissions(res.data || []));
  }, []);


  const handleFinish = (values: any) => {
    // Validate passwords
    if (password !== confirmPassword) {
      alert('Passwords do not match!');
      return;
    }

    // Prepare data
    const data = values;

    // Submit data
    if (isEdit && id) {
      // Update existing admin
      editAdmin(id, data).then((response: any) => {
        if (response.statusCode === 201) {
          message.success('Admin updated successfully!');
          navigate('/admin');
        } else if (response.statusCode === 203) {
          message.error(response.message);
        }
      }).catch((err: any) => {
        console.error(err);
        if (err.response && err.response.data && err.response.data.validationErrors) {
          const validationErrors = err.response.data.validationErrors;
          validationErrors.forEach((error: { key: string; value: string }) => {
            form.setFields([{
              name: error.key,
              errors: [error.value],
            }]);
          });
        } else {
          message.error('Failed to update admin.');
        }
      });
    } else if (!isEdit) {
      // Create new admin
      createAdmin(data).then((response: any) => {
        console.log("response is : ",response);
        if (response.statusCode === 201) {
          message.success('Admin created successfully!');
          setTimeout(() => {
            navigate('/');
          }, 1000);
        } else if (response.statusCode === 203) {
          message.error(response.message);
        }
      }).catch((err: any) => {
        console.log("err is : ",err);
        if (err && err.data && err.data.validationErrors) {
          console.log("err.data.validationErrors is : ",err.data.validationErrors);
          const validationErrors = err.data.validationErrors;
          validationErrors.forEach((error: { key: string; value: string }) => {
            console.log("error is : ",error);
            form.setFields([{
              name: error.key,
              errors: [error.value],
            }]);
          });
        } else {
          message.error('Failed to create admin.');
        }
      });
    }
  }

  useEffect(() => {
    if (isEdit && id) {
      getAdminById(id).then(res => {
        setSelectedUser(res);
        setPermissions(res.permissions?.map((p: any) => p.id) || []);
        setPassword('');
        setConfirmPassword('');
      });
    }
  }, [id, isEdit]);

  return (
    <Card style={{ padding: 32, maxWidth: 900, margin: '40px auto', border:'none' }}>
      <Typography.Title level={5} style={{ fontWeight: 700, marginBottom: 24 }}>
        <span style={{ color: '#337ab7', fontWeight: 700 }}>
          {isEdit ? `ویرایش ادمین ${id}` : 'ادمین جدید'}
        </span>
        <span style={{ color: '#bdbdbd', fontWeight: 400, fontSize: 22, marginRight: 8 }}>{' > '}</span>
      </Typography.Title>
      <Form form={form} onFinish={handleFinish} layout="vertical" style={{ maxWidth: 600, margin: '0 auto', textAlign: 'right' }}>
        <Form.Item label="نام کاربر" name="Id">
          <Select
            showSearch
            value={selectedUser ? selectedUser.id : undefined}
            placeholder="انتخاب کنید"
            optionFilterProp="children"
            onChange={id => {
              const user = users.find(u => u.id === id);
              setSelectedUser(user);
            }}
            onSearch={handleUserSearch}
            filterOption={false}
            disabled={isEdit}
            style={{ width: '100%', minWidth: 220 }}
            notFoundContent={userLoading ? <span>در حال بارگذاری...</span> : null}
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
        </Form.Item>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item label="نقش‌ها" name="RoleIds">
              <Select
                mode="multiple"
                value={selectedRoles}
                onChange={setSelectedRoles}
                placeholder="انتخاب کنید"
                style={{ width: '100%' }}
                optionLabelProp="label"
              >
                {roles.map((role, idx) => (
                  <Select.Option key={role.id} value={role.id} label={role.displayName}>
                    {role.name}
                  </Select.Option>
                ))}
              </Select>
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="دسترسی‌ها" name="PermissionIds">
              <Select
                mode="multiple"
                value={selectedPermissions}
                onChange={setSelectedPermissions}
                placeholder="انتخاب کنید"
                style={{ width: '100%' }}
                optionLabelProp="label"
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
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item label="رمز عبور جدید" name="Password">
              <Input.Password
                value={password}
                onChange={e => setPassword(e.target.value)}
                placeholder="رمز عبور جدید را وارد کنید"
              />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="تکرار رمز عبور جدید" name="ConfirmPassword">
              <Input.Password
                value={confirmPassword}
                onChange={e => setConfirmPassword(e.target.value)}
                placeholder="تکرار رمز عبور جدید را وارد کنید"
              />
            </Form.Item>
          </Col>
        </Row>
        <Form.Item>
          <Button htmlType='submit' type="primary" style={{ minWidth: 100, borderRadius: 8 }}>
            ذخیره
          </Button>
        </Form.Item>
      </Form>
    </Card>
  );
}