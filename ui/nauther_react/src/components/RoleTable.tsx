import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getRoles } from '../services/roleService';
import { Table, Input, Typography } from 'antd';
import { SearchOutlined } from '@ant-design/icons';
import AdminPopover from './AdminPopover';
import PermissionPopover from './PermissionPopover';
import type { TableRowSelection } from 'antd/lib/table/interface';

interface RoleTableProps {
  rowSelection?: TableRowSelection<any>;
}

export default function   RoleTable({ rowSelection }: RoleTableProps) {
  const [roles, setRoles] = useState<any[]>([]);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [search, setSearch] = useState('');
  const [loading, setLoading] = useState(true);
  const [adminPopoverAnchor, setAdminPopoverAnchor] = useState<HTMLElement | null>(null);
  const [permissionPopoverAnchor, setPermissionPopoverAnchor] = useState<HTMLElement | null>(null);

  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    getRoles(page + 1, rowsPerPage, search)
      .then(res => {
        console.log("res is : ",res);
        if (Array.isArray(res.data)) {
          setRoles(res.data);
          setTotal(res.data.length);
          console.log("here is the total : ",res.data.length);
        } else if (res.data && typeof res.data === 'object') {
          setRoles((res.data as any).items || []);
          setTotal((res.data as any).total || 0);
        } else {
          setRoles([]);
          setTotal(0);
        }
      })
      .finally(() => setLoading(false));
  }, [page, rowsPerPage, search]);

  const handlePermissionClick = (event: React.MouseEvent<HTMLElement>) => {
    setPermissionPopoverAnchor(event.currentTarget);
  };

  const handlePermissionPopoverClose = () => {
    setPermissionPopoverAnchor(null);
  };

  const handleAdminClick = (event: React.MouseEvent<HTMLElement>) => {
    setAdminPopoverAnchor(event.currentTarget);
  };

  const handleAdminPopoverClose = () => {
    setAdminPopoverAnchor(null);
  };

  const columns = [
    {
      title: 'نقش',
      dataIndex: 'name',
      key: 'name',
      align: 'right' as const,
    },
    {
      title: 'نام نمایشی',
      dataIndex: 'displayName',
      key: 'displayName',
      align: 'right' as const,
    },
    {
      title: 'دسترسی ها',
      dataIndex: 'permissions',
      key: 'permissions',
      align: 'right' as const,
      render: (permissions: string[], record: any) => (
        <span
          style={{ cursor: 'pointer', color: '#337ab7', fontWeight: 500 }}
          onClick={handlePermissionClick}
        >
          {permissions && permissions.length > 0 ? permissions[0] : ''}
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
      ),
    },
    {
      title: 'ادمین های نقش',
      dataIndex: 'admins',
      key: 'admins',
      align: 'right' as const,
      render: (admins: string[], record: any) => (
        <span
          style={{ cursor: 'pointer', color: '#337ab7', fontWeight: 500 }}
          onClick={handleAdminClick}
        >
          {admins && admins.length > 0 ? admins[0] : ''}
          {admins && admins.length > 1 && (
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
              + {admins.length - 1}
            </span>
          )}
        </span>
      ),
    },
  ];

  return (
    <>
      <div style={{ display: 'flex', gap: 16, marginBottom: 16, alignItems: 'center', flexWrap: 'wrap', direction: 'rtl' }}>
        <Input
          size="small"
          placeholder="جستجو نقش"
          value={search}
          onChange={e => setSearch(e.target.value)}
          style={{ width: 200 }}
          prefix={<SearchOutlined style={{ fontSize: 16 }} />}
        />
      </div>
      <Table
        rowKey="id"
        columns={columns}
        dataSource={roles}
        loading={loading}
        locale={{ emptyText: 'داده‌ای وجود ندارد.' }}
        rowSelection={rowSelection}
        pagination={{
          current: page + 1,
          pageSize: rowsPerPage,
          total: total,
          showSizeChanger: true,
          pageSizeOptions: [5, 10, 20, 50],
          onChange: (page, pageSize) => {
            setPage((page as number) - 1);
            setRowsPerPage(pageSize as number);
          },
          showTotal: (total) => `تعداد کل: ${total}`,
          position: ['bottomCenter'],
          locale: { items_per_page: 'در صفحه' },
        }}
        onRow={record => ({
          onDoubleClick: () => navigate(`/role/edit/${record.id}`),
        })}
        style={{ direction: 'rtl' }}
      />
      <AdminPopover
        anchorEl={adminPopoverAnchor}
        open={Boolean(adminPopoverAnchor)}
        onClose={handleAdminPopoverClose}
      />
      <PermissionPopover
        anchorEl={permissionPopoverAnchor}
        open={Boolean(permissionPopoverAnchor)}
        onClose={handlePermissionPopoverClose}
      />
    </>
  );
}
