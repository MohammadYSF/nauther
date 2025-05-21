import { Typography, Input, Button, Select, Card, Form, Avatar, List, Popover } from 'antd';
import { SearchOutlined, ArrowDownOutlined } from '@ant-design/icons';
import Sidebar from '../components/Sidebar';
import Topbar from '../components/Topbar';
import { useEffect, useState, useRef } from 'react';
import { useParams } from 'react-router-dom';
import { getAdminById, getAdmins } from '../services/adminService';
import { getRoles } from '../services/roleService';

export default function AdminNewPage() {
  const { id } = useParams();
  const isEdit = Boolean(id);

  const [users, setUsers] = useState<any[]>([]);
  const [selectedUser, setSelectedUser] = useState<any>(null);
  const [permissions, setPermissions] = useState<string[]>([]);
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [userDropdown, setUserDropdown] = useState(false);
  const [userSearch, setUserSearch] = useState('');
  const anchorRef = useRef<HTMLDivElement>(null);
  const [roles, setRoles] = useState<any[]>([]);

  useEffect(() => {
    getAdmins().then(res => setUsers(res.data || []));
  }, []);

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

  useEffect(() => {
    getRoles().then(res => setRoles(res.data || []));
  }, []);

  const filteredUsers = users.filter(
    u =>
      u.name.includes(userSearch) ||
      u.id.includes(userSearch)
  );

  return (

          <Card style={{ padding: 32, maxWidth: 900, margin: '40px auto', borderRadius: 16 }}>
            <Typography.Title level={5} style={{ fontWeight: 700, marginBottom: 24 }}>
              <span style={{ color: '#337ab7', fontWeight: 700 }}>
                {isEdit ? `ویرایش ادمین ${id}` : 'ادمین جدید'}
              </span>
              <span style={{ color: '#bdbdbd', fontWeight: 400, fontSize: 22, marginRight: 8 }}>{' > '}</span>
            </Typography.Title>
            <Form layout="vertical" style={{ maxWidth: 600, margin: '0 auto', textAlign: 'right' }}>
              <Form.Item label="نام کاربر">
                <Popover
                  content={
                    <div style={{ maxHeight: 220, overflowY: 'auto', minWidth: 220 }}>
                      <Input
                        size="small"
                        placeholder="جستجو"
                        value={userSearch}
                        onChange={e => setUserSearch(e.target.value)}
                        prefix={<SearchOutlined />}
                        style={{ marginBottom: 8 }}
                      />
                      <List
                        dataSource={filteredUsers}
                        renderItem={user => (
                          <List.Item
                            key={user.id}
                            onClick={() => {
                              setSelectedUser(user);
                              setUserDropdown(false);
                            }}
                            style={{ cursor: 'pointer', display: 'flex', alignItems: 'center' }}
                          >
                            <Typography.Text style={{ color: '#888', fontSize: 13, minWidth: 60 }}>{user.id}</Typography.Text>
                            <Avatar style={{ width: 24, height: 24, margin: '0 8px' }} src={user.avatar} />
                            <Typography.Text style={{ fontWeight: 500, fontSize: 15 }}>{user.name}</Typography.Text>
                          </List.Item>
                        )}
                      />
                    </div>
                  }
                  trigger="click"
                  open={userDropdown}
                  onOpenChange={setUserDropdown}
                  getPopupContainer={() => anchorRef.current || document.body}
                >
                  <div
                    ref={anchorRef}
                    style={{
                      display: 'flex',
                      alignItems: 'center',
                      border: userDropdown ? '1.5px solid #337ab7' : '1px solid #e0e0e0',
                      borderRadius: 8,
                      minHeight: 44,
                      background: isEdit ? '#f5f5f5' : 'inherit',
                      cursor: isEdit ? 'not-allowed' : 'pointer',
                      padding: '0 12px',
                      marginBottom: 16,
                    }}
                    onClick={() => {
                      if (!isEdit) setUserDropdown(prev => !prev);
                    }}
                  >
                    {selectedUser ? (
                      <>
                        <Typography.Text style={{ fontWeight: 500, marginLeft: 8 }}>{selectedUser.name}</Typography.Text>
                        <Avatar style={{ width: 24, height: 24, marginLeft: 8 }} src={selectedUser.avatar} />
                        <Typography.Text style={{ color: '#888', fontSize: 13 }}>{selectedUser.id}</Typography.Text>
                      </>
                    ) : (
                      <Typography.Text style={{ color: '#aaa' }}>انتخاب کنید</Typography.Text>
                    )}
                    <ArrowDownOutlined style={{ color: '#888', marginRight: 'auto' }} />
                  </div>
                </Popover>
              </Form.Item>
              <Form.Item label="دسترسی‌ها">
                <Select
                  mode="multiple"
                  value={permissions}
                  onChange={setPermissions}
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
              <Form.Item label="رمز عبور جدید">
                <Input.Password
                  value={password}
                  onChange={e => setPassword(e.target.value)}
                  placeholder="رمز عبور جدید را وارد کنید"
                />
              </Form.Item>
              <Form.Item label="تکرار رمز عبور جدید">
                <Input.Password
                  value={confirmPassword}
                  onChange={e => setConfirmPassword(e.target.value)}
                  placeholder="تکرار رمز عبور جدید را وارد کنید"
                />
              </Form.Item>
              <Form.Item>
                <Button type="primary" style={{ minWidth: 100, borderRadius: 8 }}>
                  ذخیره
                </Button>
              </Form.Item>
            </Form>
          </Card>

  );
}