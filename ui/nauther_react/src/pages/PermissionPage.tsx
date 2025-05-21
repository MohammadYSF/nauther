import React, { useEffect, useState } from 'react';
import { Table, Input, Button, Spin, Card } from 'antd';
import { EditOutlined, SaveOutlined, PlusOutlined, CloseOutlined } from '@ant-design/icons';
import { getPermissions, createPermission, editPermission, type GetPermissionsResponseDataModel } from '../services/permissionService';
import type { ApiError } from '../services/api';

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

  useEffect(() => {
    fetchPermissions();
  }, []);

  const fetchPermissions = async () => {
    setLoading(true);
    getPermissions().then(res => {
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

  const handleEdit = (id: string, name: string, displayName: string) => {
    setEditId(id);
    setEditValue(name);
    setEditValueDisplayName(displayName);
  };

  const handleEditSave = async (id: string) => {
    setSaving(true);
    await editPermission(id, { name: editValue, displayName: editValueDisplayName });
    setEditId(null);
    setEditValue('');
    setEditValueDisplayName('');
    fetchPermissions();
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
    fetchPermissions();
    setSaving(false);
  };

  const columns = [
    {
      title: 'نام دسترسی',
      dataIndex: 'name',
      key: 'name',
      align: 'right' as const,
      render: (text: string, record: any) =>
        editId === record.id ? (
          <Input
            size="small"
            value={editValue}
            onChange={e => setEditValue(e.target.value)}
            autoFocus
            style={{ minWidth: 120 }}
          />
        ) : (
          text
        ),
    },
    {
      title: 'نام نمایشی',
      dataIndex: 'displayName',
      key: 'displayName',
      align: 'right' as const,
      render: (text: string, record: any) =>
        editId === record.id ? (
          <Input
            size="small"
            value={editValueDisplayName}
            onChange={e => setEditValueDisplayName(e.target.value)}
            autoFocus
            style={{ minWidth: 120 }}
          />
        ) : (
          text
        ),
    },
    {
      title: '',
      key: 'actions',
      align: 'right' as const,
      render: (_: any, record: any) =>
        editId === record.id ? (
          <>
            <Button
              icon={<SaveOutlined />}
              type="primary"
              size="small"
              shape="circle"
              aria-label="ذخیره"
              onClick={() => handleEditSave(record.id)}
              loading={saving}
              style={{ marginLeft: 8 }}
            />
            <Button
              icon={<CloseOutlined />}
              type="default"
              size="small"
              shape="circle"
              aria-label="انصراف"
              danger
              onClick={() => setEditId(null)}
              disabled={saving}
            />
          </>
        ) : (
          <Button
            icon={<EditOutlined />}
            type="primary"
            size="small"
            shape="circle"
            aria-label="ویرایش"
            onClick={() => handleEdit(record.id, record.name, record.displayName)}
          />
        ),
    },
  ];

  return (

          <Card style={{ padding: 32, maxWidth: 900, margin: '40px auto',border:'none' }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', width: '100%', marginBottom: 16 }}>
              <h2 style={{ margin: 0 }}>مدیریت دسترسی‌ها</h2>
              <Button
                type="primary"
                icon={<PlusOutlined />}
                shape="circle"
                aria-label="افزودن دسترسی"
                onClick={handleAdd}
                disabled={addMode}
                style={{ borderRadius: 8 }}
              />
            </div>
            {addMode && (
              <div style={{ display: 'flex', gap: 8, marginBottom: 16 }}>
                <Input
                  size="small"
                  value={addValue}
                  onChange={e => setAddValue(e.target.value)}
                  placeholder="نام دسترسی"
                  autoFocus
                  style={{ minWidth: 120 }}
                />
                <Input
                  size="small"
                  value={addValueDisplayName}
                  onChange={e => setAddValueDisplayName(e.target.value)}
                  placeholder="نام نمایشی"
                  style={{ minWidth: 120 }}
                />
                <Button
                  icon={<SaveOutlined />}
                  type="primary"
                  size="small"
                  shape="circle"
                  aria-label="ذخیره"
                  onClick={handleAddSave}
                  loading={saving}
                  style={{ marginLeft: 8 }}
                />
                <Button
                  icon={<CloseOutlined />}
                  type="default"
                  size="small"
                  shape="circle"
                  aria-label="انصراف"
                  danger
                  onClick={() => setAddMode(false)}
                  disabled={saving}
                />
              </div>
            )}
            <Spin spinning={loading} tip="در حال بارگذاری...">
              <Table
                rowKey="id"
                columns={columns}
                dataSource={permissions?.data || []}
                pagination={false}
                style={{ direction: 'rtl' }}
                locale={{ emptyText: 'داده‌ای وجود ندارد.' }}
              />
            </Spin>
          </Card>

  );
}