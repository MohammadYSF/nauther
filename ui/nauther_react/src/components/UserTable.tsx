import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getAdmins } from '../services/adminService';
import { Table, Input, Select, Checkbox, Avatar, Popover, List, Typography, Divider } from 'antd';
import { SearchOutlined } from '@ant-design/icons';
import { SafetyCertificateOutlined, TeamOutlined } from '@ant-design/icons';
import RolePopover from './RolePopover';
import { getAllUsers, type User } from '../services/userService';

const rolesList = ['سوپر ادمین', 'ادمین', 'کاربر عادی'];

export default function UserTable({ selected, setSelected, refresh }: { selected: string[], setSelected: React.Dispatch<React.SetStateAction<string[]>>, refresh: boolean }) {
  const [users, setUsers] = useState<any[]>([]);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [search, setSearch] = useState('');
  const [loading, setLoading] = useState(true);
  const [popoverAnchor, setPopoverAnchor] = useState<HTMLElement | null>(null);
  const [roleFilter, setRoleFilter] = useState('');

  const navigate = useNavigate();


  
  useEffect(() => {
    setLoading(true);
    getAllUsers(page + 1, rowsPerPage,'')
      .then(res => {
        if (Array.isArray(res.data)) {
          setUsers(res.data);
          setTotal(res.data.length);
        } else if (res.data && typeof res.data === 'object') {
          setUsers((res.data as any).items || []);
          setTotal((res.data as any).total || 0);
        } else {
          setUsers([]);
          setTotal(0);
        }
      })
      .finally(() => setLoading(false));
  }, [page, rowsPerPage, search, refresh]);

  const handleSelectAll = (checked: boolean) => {
    if (checked) {
      setSelected(users.map((u: any) => u.id));
    } else {
      setSelected([]);
    }
  };

  const handleSelectRow = (id: string) => {
    setSelected(prev =>
      prev.includes(id) ? prev.filter(sid => sid !== id) : [...prev, id]
    );
  };

  const isSelected = (id: string) => selected.includes(id);

  const handleChangePage = (page: number, pageSize: number) => {
    setPage(page - 1);
    setRowsPerPage(pageSize);
  };

  const handleRoleClick = (event: React.MouseEvent<HTMLElement>) => {
    setPopoverAnchor(event.currentTarget);
  };

  const handlePopoverClose = () => {
    setPopoverAnchor(null);
  };

  const columns = [
    {
      title: (
        <Checkbox
          checked={users.length > 0 && selected.length === users.length}
          indeterminate={selected.length > 0 && selected.length < users.length}
          onChange={e => handleSelectAll(e.target.checked)}
        />
      ),
      dataIndex: 'id',
      key: 'checkbox',
      width: 50,
      render: (_: any, record: any) => (
        <Checkbox
          checked={isSelected(record.id)}
          onChange={() => handleSelectRow(record.id)}
        />
      ),
    },
    {
      title: 'وضعیت',
      dataIndex: 'isActive',
      key: 'isActive',
      width: 120,
      render: (isActive: boolean) => (
        <div style={{ display: 'flex', alignItems: 'center', height: 48 }}>
          <div
            style={{
              width: 4,
              height: 34,
              borderRadius: 2,
              background: isActive ? '#21C362' : '#F44336',
              marginRight: 0,
            }}
          />
          <span dir="rtl" style={{ fontWeight: 500, marginRight: 20 }}>
            {isActive ? 'فعال' : 'غیرفعال'}
          </span>
        </div>
      ),
    },
    {
      title: 'شناسه',
      dataIndex: 'userCode',
      key: 'userCode',
      align: 'right' as const,
    },
    {
      title: 'نام و نام خانوادگی',
      dataIndex: 'username',
      key: 'username',
      align: 'right' as const,
      render: (name: string, record: any) => (
        <div style={{ display: 'inline-flex', alignItems: 'center', justifyContent: 'end' }}>
          <Avatar style={{ marginLeft: 8, width: 24, height: 24 }} />
          {name}
        </div>
      ),
    },
    {
      title: 'شماره موبایل',
      dataIndex: 'phoneNumber',
      key: 'phoneNumber',
      align: 'right' as const,
    },
    {
      title: 'نقش‌ها',
      dataIndex: 'roles',
      key: 'roles',
      align: 'right' as const,
      render: (roles: { displayName: string }[] = []) => {
        const content = (
          <div style={{ minWidth: 200, padding: 8, direction: 'rtl' }}>
            <List
              size="small"
              dataSource={roles}
              renderItem={role => (
                <List.Item style={{ padding: '6px 0', border: 'none', display: 'flex', alignItems: 'center', justifyContent: 'flex-end', flexDirection: 'row-reverse', textAlign: 'right' }}>
                  <Typography.Text style={{ fontWeight: 500 }}>{role.displayName}</Typography.Text>
                  <TeamOutlined style={{ color: '#1976d2', fontSize: 16, marginRight: 8 }} />
                </List.Item>
              )}
              style={{ margin: 0, background: '#f7fafd', borderRadius: 8, boxShadow: '0 2px 8px #e3e3e3', textAlign: 'right' }}
            />
          </div>
        );
        return (
          <Popover content={content} trigger="click" placement="bottomRight">
            <span style={{ cursor: 'pointer', color: '#337ab7', fontWeight: 500 }}>
              {roles && roles.length > 0 ? roles[0].displayName : ''}
              {roles && roles.length > 1 && (
                <span
                  style={{
                    background: '#e3f2fd',
                    color: '#1976d2',
                    fontSize: 10,
                    borderRadius: 4,
                    padding: '0 4px',
                    marginLeft: 8,
                    height: 20,
                    display: 'inline-flex',
                    alignItems: 'center',
                  }}
                >
                  + {roles.length - 1}
                </span>
              )}
            </span>
          </Popover>
        );
      },
    },
    {
      title: 'دسترسی‌ها',
      dataIndex: 'permissions',
      key: 'permissions',
      align: 'right' as const,
      render: (permissions: { displayName: string }[] = []) => {
        const content = (
          <div style={{ minWidth: 200, padding: 8, direction: 'rtl' }}>
            <List
              size="small"
              dataSource={permissions}
              renderItem={perm => (
                <List.Item style={{ padding: '6px 0', border: 'none', display: 'flex', alignItems: 'center', justifyContent: 'flex-end', flexDirection: 'row-reverse', textAlign: 'right' }}>
                  <Typography.Text style={{ fontWeight: 500 }}>{perm.displayName}</Typography.Text>
                  <SafetyCertificateOutlined style={{ color: '#1976d2', fontSize: 16, marginRight: 8 }} />
                </List.Item>
              )}
              style={{ margin: 0, background: '#f7fafd', borderRadius: 8, boxShadow: '0 2px 8px #e3e3e3', textAlign: 'right' }}
            />
          </div>
        );
        return (
          <Popover content={content} trigger="click" placement="bottomRight">
            <span style={{ cursor: 'pointer', color: '#337ab7', fontWeight: 500 }}>
              {permissions && permissions.length > 0 ? permissions[0].displayName : ''}
              {permissions && permissions.length > 1 && (
                <span
                  style={{
                    background: '#e3f2fd',
                    color: '#1976d2',
                    fontSize: 10,
                    borderRadius: 4,
                    padding: '0 4px',
                    marginLeft: 8,
                    height: 20,
                    display: 'inline-flex',
                    alignItems: 'center',
                  }}
                >
                  + {permissions.length - 1}
                </span>
              )}
            </span>
          </Popover>
        );
      },
    }    
  ];

  return (
    <>
      <div style={{ display: 'flex', gap: 16, marginBottom: 16, alignItems: 'center', flexWrap: 'wrap', direction: 'rtl' }}>
        <Input
          size="small"
          placeholder="جستجو نام یا کد"
          value={search}
          onChange={e => setSearch(e.target.value)}
          style={{ width: 200 }}
          prefix={<SearchOutlined style={{ fontSize: 16 }} />}
        />
        <Select
          size="small"
          style={{ minWidth: 140 }}
          value={roleFilter}
          onChange={setRoleFilter}
          placeholder="همه نقش‌ها"
          allowClear
        >
          <Select.Option value="">همه نقش‌ها</Select.Option>
          {rolesList.map((role, idx) => (
            <Select.Option key={idx} value={role}>{role}</Select.Option>
          ))}
        </Select>
      </div>
      <Table
        rowKey="id"
        columns={columns}
        dataSource={users}
        loading={loading}
        locale={{ emptyText: 'داده‌ای وجود ندارد.' }}

        pagination={{
          current: page + 1,
          pageSize: rowsPerPage,
          total: total,
          showSizeChanger: true,
          
          pageSizeOptions: [5, 10, 20, 50],
          onChange: handleChangePage,
          showTotal: (total) => `تعداد کل: ${total}`,
          position: ['bottomCenter'],
        }}
        onRow={record => ({
          onDoubleClick: () => navigate(`/admin/edit/${record.id}`),
        })}
        style={{ direction: 'rtl' }}
        rowClassName={record => (isSelected(record.id) ? 'ant-table-row-selected' : '')}
      />
      <RolePopover
        anchorEl={popoverAnchor}
        open={Boolean(popoverAnchor)}
        onClose={handlePopoverClose}
      />
    </>
  );
}
