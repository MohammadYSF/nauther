import React, { useEffect, useState, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import { getRoles } from '../services/roleService';
import { Table, Input, Typography, Popover, List, Button } from 'antd';
import { SearchOutlined, SafetyCertificateOutlined, UserOutlined } from '@ant-design/icons';
import AdminPopover from './AdminPopover';
import PermissionPopover from './PermissionPopover';
import type { TableRowSelection } from 'antd/lib/table/interface';
import debounce from 'lodash.debounce';

interface RoleTableProps {
  rowSelection?: TableRowSelection<any>;
  refresh: boolean;
}

export default function   RoleTable({ rowSelection, refresh }: RoleTableProps) {
  const [roles, setRoles] = useState<any[]>([]);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [search, setSearch] = useState('');
  const [loading, setLoading] = useState(true);
  const [adminPopoverAnchor, setAdminPopoverAnchor] = useState<HTMLElement | null>(null);
  const [permissionPopoverAnchor, setPermissionPopoverAnchor] = useState<HTMLElement | null>(null);

  const navigate = useNavigate();

  // Debounced search function
  const debouncedSearch = useRef(
    debounce((value: string) => {
      setSearch(value);
    }, 500)
  ).current;

  useEffect(() => {
    setLoading(true);
    getRoles({page:page + 1,pageSize: rowsPerPage,search:search})
      .then(res => {
        if (Array.isArray(res.data)) {
          setRoles(res.data);
          setTotal(res.metadata['total']);
        } else if (res.data && typeof res.data === 'object') {
          setRoles((res.data as any).items || []);
          setTotal((res.data as any).metadata.total || 0);
        } else {
          setRoles([]);
          setTotal(0);
        }
      })
      .finally(() => setLoading(false));
  }, [page, rowsPerPage, search, refresh]);

  // Cleanup debounce on unmount
  useEffect(() => {
    return () => {
      debouncedSearch.cancel();
    };
  }, [debouncedSearch]);

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
      render: (permissions: { id: string, name?: string, displayName: string }[] = [], record: any) => {
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
            <span
              style={{ cursor: 'pointer', color: '#337ab7', fontWeight: 500 }}
            >
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
    },
    {
      title: 'ادمین های نقش',
      dataIndex: 'users',
      key: 'users',
      align: 'right' as const,
      render: (users:{id:string,name:string}[], record: any) => {
        const content = (
          <div style={{ minWidth: 200, padding: 8, direction: 'rtl' }}>
            <List
              size="small"
              dataSource={users}
              renderItem={user => (
                <List.Item style={{ padding: '6px 0', border: 'none', display: 'flex', alignItems: 'center', justifyContent: 'flex-end', flexDirection: 'row-reverse', textAlign: 'right' }}>
                  <Typography.Text style={{ fontWeight: 500 }}>{user.name}</Typography.Text>
                  <UserOutlined style={{ color: '#1976d2', fontSize: 16, marginRight: 8 }} />
                </List.Item>
              )}
              style={{ margin: 0, background: '#f7fafd', borderRadius: 8, boxShadow: '0 2px 8px #e3e3e3', textAlign: 'right' }}
            />
          </div>
        );
        return (
          <Popover content={content} trigger="click" placement="bottomRight">
            <span
              style={{ cursor: 'pointer', color: '#337ab7', fontWeight: 500 }}
            >
              {users && users.length > 0 ? users[0].name : ''}
              {users && users.length > 1 && (
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
                  + {users.length - 1}
                </span>
              )}
            </span>
          </Popover>
        );
      },
    },
  ];

  const handleChangePage = (page: number, pageSize: number) => {
    setPage((page as number) - 1);
    setRowsPerPage(pageSize as number);
  };

  return (
    <>
      <div style={{ display: 'flex', gap: 16, marginBottom: 16, alignItems: 'center', flexWrap: 'wrap', direction: 'rtl' }}>
        <Input
          size="small"
          placeholder="جستجو نقش"
          onChange={e => debouncedSearch(e.target.value)}
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
          onChange: handleChangePage,
          showTotal: (total) => `تعداد کل: ${total}`,
          position: ['bottomCenter']
        }}
        onRow={record => ({
          onDoubleClick: () => navigate(`/role/edit/${record.id}`),
        })}
        style={{ direction: 'rtl' }}
      />
      {/* <AdminPopover
        anchorEl={adminPopoverAnchor}
        open={Boolean(adminPopoverAnchor)}
        onClose={handleAdminPopoverClose}
      />
      <PermissionPopover
        anchorEl={permissionPopoverAnchor}
        open={Boolean(permissionPopoverAnchor)}
        onClose={handlePermissionPopoverClose}
      /> */}
    </>
  );
}
