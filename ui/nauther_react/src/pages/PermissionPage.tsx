import React, { useEffect, useState } from 'react';
import {
  Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper,
  IconButton, TextField, Button, Box, CircularProgress
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import SaveIcon from '@mui/icons-material/Save';
import AddIcon from '@mui/icons-material/Add';
import CancelIcon from '@mui/icons-material/Close';
import { getPermissions, createPermission, editPermission, type GetPermissionsResponseDataModel } from '../services/permissionService';
import Sidebar from '../components/Sidebar';
import Topbar from '../components/Topbar';
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

  const handleEdit = (id: string, name: string) => {
    setEditId(id);
    setEditValue(name);
  };

  const handleEditSave = async (id: string) => {
    setSaving(true);
    await editPermission(id, { name: editValue,displayName:editValueDisplayName });
    setEditId(null);
    setEditValue('');
    fetchPermissions();
    setSaving(false);
  };

  const handleAdd = () => {
    setAddMode(true);
    setAddValue('');
  };

  const handleAddSave = async () => {
    if (!addValue.trim()) return;
    setSaving(true);
    await createPermission({ name: addValue ,displayName:addValueDisplayName});
    setAddMode(false);
    setAddValue('');
    fetchPermissions();
    setSaving(false);
  };

  return (
    <Box sx={{ bgcolor: '#f4f6f8', minHeight: '100vh', width: '100vw', p: 0, m: 0 }}>
      <Box sx={{ width: '100vw', minHeight: '100vh', position: 'relative' }}>
        <Sidebar />
        <Box sx={{ pr: 7 }}>
          <Topbar />
          <Box sx={{ p: 4, pt: 5, maxWidth: 900, mx: 'auto', display: 'flex', flexDirection: 'column', alignItems: 'flex-end' }}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', width: '100%', mb: 2 }}>
              <h2 style={{ margin: 0 }}>مدیریت دسترسی‌ها</h2>
              <Button
                variant="contained"
                startIcon={<AddIcon />}
                onClick={handleAdd}
                disabled={addMode}
              >
                افزودن دسترسی
              </Button>
            </Box>
            <TableContainer component={Paper}>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell align="right">نام دسترسی</TableCell>
                    <TableCell align="right" width={120}></TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {addMode && (
                    <TableRow>
                      <TableCell align="right">
                        <TextField
                          size="small"
                          value={addValue}
                          onChange={e => setAddValue(e.target.value)}
                          autoFocus
                        />
                      </TableCell>
                      <TableCell align="right">
                        <TextField
                          size="small"
                          value={addValueDisplayName}
                          onChange={e => setAddValueDisplayName(e.target.value)}
                          autoFocus
                        />
                      </TableCell>
                      <TableCell align="right">
                        <IconButton color="primary" onClick={handleAddSave} disabled={saving}>
                          <SaveIcon />
                        </IconButton>
                        <IconButton color="error" onClick={() => setAddMode(false)} disabled={saving}>
                          <CancelIcon />
                        </IconButton>
                      </TableCell>
                    </TableRow>
                  )}
                  {loading ? (
                    <TableRow>
                      <TableCell colSpan={2} align="center">
                        <CircularProgress size={24} />
                      </TableCell>
                    </TableRow>
                  ) : permissions?.data.length === 0 ? (
                    <TableRow>
                      <TableCell colSpan={2} align="center">
                        داده‌ای وجود ندارد.
                      </TableCell>
                    </TableRow>
                  ) : (
                    permissions?.data.map((perm) => (
                      <TableRow key={perm.id}>
                        <TableCell align="right">
                          {editId === perm.id ? (
                            <TextField
                              size="small"
                              value={editValue}
                              onChange={e => setEditValue(e.target.value)}
                              autoFocus
                            />
                          ) : (
                            perm.name
                          )}
                        </TableCell>
                        <TableCell align="right">
                          {editId === perm.id ? (
                            <TextField
                              size="small"
                              value={editValueDisplayName}
                              onChange={e => setEditValueDisplayName(e.target.value)}
                              autoFocus
                            />
                          ) : (
                            perm.displayName
                          )}
                        </TableCell>
                        <TableCell align="right">
                          {editId === perm.id ? (
                            <>
                              <IconButton color="primary" onClick={() => handleEditSave(perm.id)} disabled={saving}>
                                <SaveIcon />
                              </IconButton>
                              <IconButton color="error" onClick={() => setEditId(null)} disabled={saving}>
                                <CancelIcon />
                              </IconButton>
                            </>
                          ) : (
                            <IconButton color="primary" onClick={() => handleEdit(perm.id, perm.name)}>
                              <EditIcon />
                            </IconButton>
                          )}
                        </TableCell>
                      </TableRow>
                    ))
                  )}
                </TableBody>
              </Table>
            </TableContainer>
          </Box>
        </Box>
      </Box>
    </Box>
  );
}