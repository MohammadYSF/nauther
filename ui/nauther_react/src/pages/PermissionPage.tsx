import React, { useEffect, useState, useRef } from 'react';
import { Table, Input, Button, Spin, Card, Affix, message } from 'antd';
import { EditOutlined, SaveOutlined, PlusOutlined, CloseOutlined, SearchOutlined } from '@ant-design/icons';
import { getPermissions, createPermission, editPermission, type GetPermissionsResponseDataModel, deletePermissions } from '../services/permissionService';
import type { ApiError } from '../services/api';
import { useClickAway } from 'react-use';
import debounce from 'lodash.debounce';
import type { AxiosError } from 'axios';
import type { BaseApiResponseModel } from '../types/baseApiResponseModel';

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
  const [search, setSearch] = useState('');
  const [selectedPermissions, setSelectedPermissions] = useState<string[]>([]);
  const editRowRef = useRef<HTMLDivElement>(null);
  const addRowRef = useRef<HTMLDivElement>(null);
  const [messageApi, contextHolder] = message.useMessage();

  // Debounced search function
  const debouncedSearch = useRef(
    debounce((value: string) => {
      setSearch(value);
    }, 500)
  ).current;

  useEffect(() => {
    fetchPermissions(page, pageSize, search);
  }, [page, pageSize, search]);

  // Cleanup debounce on unmount
  useEffect(() => {
    return () => {
      debouncedSearch.cancel();
    };
  }, [debouncedSearch]);

  // useClickAway(editRowRef, () => {
  //   if (editId) {
  //     setEditId(null);
  //     setEditValue('');
  //     setEditValueDisplayName('');
  //   }
  // });

  // useClickAway(addRowRef, () => {
  //   if (addMode) {
  //     setAddMode(false);
  //     setAddValue('');
  //     setAddValueDisplayName('');
  //   }
  // });

  const fetchPermissions = async (pageNumber = 1, pageSizeNumber = 10, searchString = '') => {
    setLoading(true);
    getPermissions({ page: pageNumber, pageSize: pageSizeNumber, search: searchString }).then(res => {
      setPermissions(res);
    })
      .catch((error: AxiosError) => {
        messageApi.error((error.response?.data as BaseApiResponseModel).message)
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
    await editPermission({ id: id, name: editValue, displayName: editValueDisplayName });
    setEditId(null);
    setEditValue('');
    setEditValueDisplayName('');
    fetchPermissions(page, pageSize, search);
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
    await createPermission({ name: addValue, displayName: addValueDisplayName }).then((res) => {
      messageApi.success(res.message);
      setAddMode(false);
      setAddValue('');
      setAddValueDisplayName('');
      fetchPermissions(page, pageSize, search);
    }).catch((error: AxiosError) => {
      messageApi.error((error.response?.data as BaseApiResponseModel).message)
    }).finally(() => {
      setSaving(false);
    });
  };

  const handleDeletePermission = async (id: string) => {

    await deletePermissions({ "ids": [id] });
    fetchPermissions(page, pageSize, search);

  };

  const handleDeleteSelectedPermissions = async () => {
    await deletePermissions({ "ids": selectedPermissions }).then((res) => {
      messageApi.success(res.message);
      setSelectedPermissions([]);
      fetchPermissions(page, pageSize, search);
    })
      .catch((error) => {
        messageApi.error((error.response?.data as BaseApiResponseModel).message)
      });
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
      {contextHolder}
      <div style={{ display: 'flex', justifyContent: 'space-between', width: '100%', marginBottom: 16 }}>
        <h2 style={{ margin: 0 }}>مدیریت دسترسی‌ها</h2>
        <div style={{ display: 'flex', gap: 16, alignItems: 'center' }}>
          <Input
            size="small"
            placeholder="جستجو دسترسی"
            onChange={e => debouncedSearch(e.target.value)}
            style={{ width: 200 }}
            prefix={<SearchOutlined style={{ fontSize: 16 }} />}
          />
          <div style={{ display: 'flex', gap: 8 }}>
            {(addMode || editId) && (
              <Button
                type="default"
                danger
                style={{
                  borderRadius: 8,
                  display: 'flex',
                  alignItems: 'center',
                  gap: 8,
                  height: 32,
                  padding: '0 16px',
                  borderColor: '#ff4d4f',
                  color: '#ff4d4f'
                }}
                icon={<CloseOutlined />}
                onClick={() => {
                  if (addMode) {
                    setAddMode(false);
                    setAddValue("");
                    setAddValueDisplayName("");
                  } else if (editId) {
                    setEditId(null);
                    setEditValue("");
                    setEditValueDisplayName("");
                  }
                }}
              >
                انصراف
              </Button>
            )}
            <Button
              type={editId ? "primary" : "primary"}
              style={{
                borderRadius: 8,
                display: 'flex',
                alignItems: 'center',
                gap: 8,
                height: 32,
                padding: '0 16px',
                boxShadow: '0 2px 0 rgba(0, 0, 0, 0.045)',
                backgroundColor: editId ? '#52c41a' : '#1890ff',
                borderColor: editId ? '#52c41a' : '#1890ff'
              }}
              icon={editId ? <SaveOutlined /> : <PlusOutlined />}
              onClick={() => {
                if (editId) {
                  handleEditSave(editId);
                } else if (addMode) {
                  handleAddSave();
                } else {
                  handleAdd();
                }
              }}
            >
              {editId ? 'ذخیره' : addMode ? 'افزودن' : 'افزودن دسترسی'}
            </Button>
          </div>
        </div>
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
            pageSizeOptions: [5, 10, 20, 50],
            showTotal: (total) => `تعداد کل: ${total}`,
            position: ['bottomCenter']
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