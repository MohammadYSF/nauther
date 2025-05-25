import React, { useEffect, useState, useRef } from 'react';
import { Table, Input, Button, Spin, Card, Affix } from 'antd';
import { EditOutlined, SaveOutlined, PlusOutlined, CloseOutlined } from '@ant-design/icons';
import { getPermissions, createPermission, editPermission, type GetPermissionsResponseDataModel, deletePermissions } from '../services/permissionService';
import type { ApiError } from '../services/api';
import { useClickAway } from 'react-use';

export default function PermissionPage() {
  const [permissions, setPermissions] = useState<GetPermissionsResponseDataModel>();
  const [loading, setLoading] = useState(true);
  const [editId, setEditId] = useState<string | null>(null);
  const [editValue, setEditValue] = useState('');
  const [editValueDisplayName, setEditValueDisplayName] = useState('');
  const [addMode, setAddMode] = useState(false);
  const [addValue, setAddValue] = useState('');
  const [addValueDisplayName, setAddValueDisplayName] = useState('');
  const [saving, setSaving] = useState(false);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [selectedPermissions, setSelectedPermissions] = useState<string[]>([]);
  const editRowRef = useRef<HTMLDivElement>(null);
  const addRowRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    fetchPermissions(page, pageSize);
  }, [page, pageSize]);

  useClickAway(editRowRef, () => {
    if (editId) {
      setEditId(null);
      setEditValue('');
      setEditValueDisplayName('');
    }
  });

  useClickAway(addRowRef, () => {
    if (addMode) {
      setAddMode(false);
      setAddValue('');
      setAddValueDisplayName('');
    }
  });

  const fetchPermissions = async (pageNumber = 1, pageSizeNumber = 10) => {
    setLoading(true);
    getPermissions(pageNumber, pageSizeNumber).then(res => {
      setPermissions(res);
    })
      .catch(err => {
        let error = err as ApiError;
        console.log(error.data);
        console.log(error.message);
        console.log(error.status);
      })
      .finally(() => setLoading(false));
  };

  const handleTableChange = (pagination: any) => {
    setPage(pagination.current);
    setPageSize(pagination.pageSize);
  };

  const handleEdit = (id: string, name: string, displayName: string) => {
    setEditId(id);
    setEditValue(name);
    setEditValueDisplayName(displayName);
  };

  const handleEditSave = async (id: string) => {
    setSaving(true);
    await editPermission({id:id, name: editValue, displayName: editValueDisplayName });
    setEditId(null);
    setEditValue('');
    setEditValueDisplayName('');
    fetchPermissions(page, pageSize);
    setSaving(false);
  };

  const handleAdd = () => {
    setAddMode(true);
    setAddValue('');
    setAddValueDisplayName('');
  };

  const handleAddSave = async () => {
    if (!addValue.trim()) return;
    setSaving(true);
    await createPermission({ name: addValue, displayName: addValueDisplayName });
    setAddMode(false);
    setAddValue('');
    setAddValueDisplayName('');
    fetchPermissions(page, pageSize);
    setSaving(false);
  };

  const handleDeletePermission = async (id: string) => {
    try {
      
      await deletePermissions({"ids":[id]});
      fetchPermissions(page, pageSize);
    } catch (error) {
      console.error('Error deleting permission:', error);
    }
  };

  const handleDeleteSelectedPermissions = async () => {
    try {
      await deletePermissions({"ids":selectedPermissions});
      setSelectedPermissions([]);
      fetchPermissions(page, pageSize);
    } catch (error) {
      console.error('Error deleting permissions:', error);
    }
  };

  const columns = [
    {
      title: 'نام دسترسی',
      dataIndex: 'name',
      key: 'name',
      align: 'right' as const,
      render: (text: string, record: any) => {
        if (addMode && record.id === '__new') {
          return (
            <div ref={addRowRef} style={{ display: 'flex', gap: 8 }}>
              <Input
                size="small"
                value={addValue}
                onChange={e => setAddValue(e.target.value)}
                placeholder="نام دسترسی"
                autoFocus
                style={{ minWidth: 120 }}
                onKeyDown={e => {
                  if (e.key === 'Escape') {
                    setAddMode(false);
                    setAddValue('');
                    setAddValueDisplayName('');
                  }
                }}
              />
            </div>
          );
        }
        if (editId === record.id) {
          return (
            <div ref={editRowRef} style={{ display: 'flex', gap: 8 }}>
              <Input
                size="small"
                value={editValue}
                onChange={e => setEditValue(e.target.value)}
                autoFocus
                style={{ minWidth: 120 }}
                onKeyDown={e => {
                  if (e.key === 'Escape') {
                    setEditId(null);
                    setEditValue('');
                    setEditValueDisplayName('');
                  }
                }}
              />
            </div>
          );
        }
        return text;
      },
    },
    {
      title: 'نام نمایشی',
      dataIndex: 'displayName',
      key: 'displayName',
      align: 'right' as const,
      render: (text: string, record: any) => {
        if (addMode && record.id === '__new') {
          return (
            <Input
              size="small"
              value={addValueDisplayName}
              onChange={e => setAddValueDisplayName(e.target.value)}
              placeholder="نام نمایشی"
              style={{ minWidth: 120 }}
              onKeyDown={e => {
                if (e.key === 'Escape') {
                  setAddMode(false);
                  setAddValue('');
                  setAddValueDisplayName('');
                }
              }}
            />
          );
        }
        if (editId === record.id) {
          return (
            <Input
              size="small"
              value={editValueDisplayName}
              onChange={e => setEditValueDisplayName(e.target.value)}
              autoFocus
              style={{ minWidth: 120 }}
              onKeyDown={e => {
                if (e.key === 'Escape') {
                  setEditId(null);
                  setEditValue('');
                  setEditValueDisplayName('');
                }
              }}
            />
          );
        }
        return text;
      },
    },
  ];

  // Compose dataSource with new row if addMode
  const dataSource = addMode
    ? [{ id: '__new', name: addValue, displayName: addValueDisplayName }, ...(permissions?.data || [])]
    : permissions?.data || [];

  return (
    <Card style={{ padding: 32, maxWidth: 900, margin: '0px auto', border: 'none' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', width: '100%' }}>
        <h2 style={{ margin: 0 }}>مدیریت دسترسی‌ها</h2>
        <Button
          type={addMode ? "primary" : "primary"}
          danger={addMode ? true : false}
          style={{ borderRadius: 8 }}
          icon={addMode ? <CloseOutlined /> : <PlusOutlined />}
          shape="circle"
          aria-label={addMode ? "انصراف" : "افزودن دسترسی"}
          onClick={() => {
            if (addMode) {
              setAddMode(false);
              setAddValue("");
              setAddValueDisplayName("");
            } else {
              handleAdd();
            }
          }}
        />
      </div>
      <Spin spinning={loading} tip="در حال بارگذاری...">
        <Table
          rowKey="id"
          rowSelection={{
            selectedRowKeys: selectedPermissions,
            onChange: (selectedRowKeys) => setSelectedPermissions(selectedRowKeys as string[]),
          }}
          columns={columns}
          dataSource={dataSource}
          pagination={{
            current: page,
            pageSize: pageSize,
            total: permissions?.metadata?.total || 0,
            showSizeChanger: true,
            showTotal: (total) => `تعداد کل: ${total}`,
            position: ['bottomCenter'],
            locale: { items_per_page: 'در صفحه' },
          }}
          style={{ direction: 'rtl' }}
          locale={{ emptyText: 'داده‌ای وجود ندارد.' }}
          onChange={handleTableChange}
          onRow={record => ({
            onDoubleClick: () => {
              if (record.id !== '__new') {
                setEditId(record.id);
                setEditValue(record.name);
                setEditValueDisplayName(record.displayName);
              }
            }
          })}
        />
        <Affix offsetBottom={20}>
          <Button
            type="primary"
            danger
            disabled={selectedPermissions.length === 0}
            onClick={handleDeleteSelectedPermissions}
          >
            حذف انتخاب شده‌ها
          </Button>
        </Affix>
      </Spin>
    </Card>
  );
}