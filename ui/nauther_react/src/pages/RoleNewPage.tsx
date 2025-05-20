import {
  Box,
  Typography,
  TextField,
  Button,
  MenuItem,
  FormControl,
  Chip,
  OutlinedInput,
  Select,
} from '@mui/material';
import Sidebar from '../components/Sidebar';
import Topbar from '../components/Topbar';
import { useState, useRef, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getRoleById, getRoles } from '../services/roleService';
import { getPermissions, type GetPermissionsResponseDataModel } from '../services/permissionService';

export default function RoleNewPage() {
  const { id } = useParams();
  const isEdit = Boolean(id);

  // Fetch all roles if needed
  const [roles, setRoles] = useState<any[]>([]);
  // Fetch all permissions for selection
  const [permissions, setPermissions] = useState<GetPermissionsResponseDataModel>({ data: [], total: 0 });
  // Form state
  const [roleName, setRoleName] = useState('');
  const [displayName, setDisplayName] = useState('');
  const [selectedPermissions, setSelectedPermissions] = useState<{ id: string, name: string, displayName: string }[]>([]);

  useEffect(() => {
    getRoles().then(res => setRoles(res.data || []));
    getPermissions().then(res => setPermissions(res));
  }, []);

  useEffect(() => {
    if (isEdit && id) {
      getRoleById(id).then(res => {
        setRoleName(res.name);
        setSelectedPermissions(res.permissions);
      });
    }
  }, [id, isEdit]);

  return (
    <Box sx={{ bgcolor: '#f4f6f8', minHeight: '100vh', width: '100vw', p: 0, m: 0 }}>
      <Box sx={{ width: '100vw', minHeight: '100vh', position: 'relative' }}>
        <Sidebar />
        <Box sx={{ pr: 7 }}>
          <Topbar />
          <Box sx={{ p: 4, pt: 5, maxWidth: 900, mx: 'auto', display: 'flex', flexDirection: 'column', alignItems: 'flex-end' }}>
            <Typography variant="h6" sx={{ fontWeight: 700, mb: 3 }}>
              <span style={{ color: '#337ab7', fontWeight: 700 }}>
                {isEdit ? `ویرایش نقش ${id}` : 'نقش جدید'}
              </span>
              <span style={{ color: '#bdbdbd', fontWeight: 400, fontSize: 22, marginRight: 8 }}>{' > '}</span>
            </Typography>
            <Box
              sx={{
                display: 'flex',
                flexDirection: 'row',
                gap: 4,
                width: '100%',
                alignItems: 'flex-start',
                justifyContent: 'flex-end',
              }}
            >
              <Box sx={{ minWidth: 320, flex: 1 }}>
                <Typography sx={{ mb: 1, fontWeight: 500 }}>نام نقش</Typography>
                <TextField
                  fullWidth
                  size="small"
                  value={roleName}
                  onChange={e => setRoleName(e.target.value)}
                  placeholder="نام نقش را وارد کنید"
                  sx={{ mb: 2 }}
                />
                <Typography sx={{ mb: 1, fontWeight: 500 }}>نام نمایشی نقش</Typography>
                <TextField
                  fullWidth
                  size="small"
                  value={displayName}
                  onChange={e => setDisplayName(e.target.value)}
                  placeholder="نام نمایشی را وارد کنید"
                  sx={{ mb: 2 }}
                />
                <Typography sx={{ mb: 1, fontWeight: 500 }}>دسترسی‌ها</Typography>
                <FormControl fullWidth size="small" sx={{ mb: 2 }}>
                  <Select
                    multiple
                    displayEmpty
                    value={selectedPermissions}
                    onChange={e => {
                      let x = e.target.value;
                      setSelectedPermissions(x as {id:string,name:string,displayName:string}[]);
                    }}
                    input={<OutlinedInput />}
                    renderValue={selected =>
                      (selected).length === 0
                        ? <em>انتخاب کنید</em>
                        : (
                          <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
                            {(selected).map(item => (
                              <Chip key={item.id} label={permissions.data.find(a => a.id == item.id)?.displayName} />
                            ))}
                          </Box>
                        )
                    }
                  >
                    <MenuItem disabled value="">
                      <em>انتخاب کنید</em>
                    </MenuItem>
                    {permissions.data.map((perm, idx) => (
                      <MenuItem key={idx} value={perm.id}>{perm.name}</MenuItem>
                    ))}
                  </Select>
                </FormControl>
                <Button variant="contained" color="primary" sx={{ mt: 1, borderRadius: 2, minWidth: 100 }}>
                  ذخیره
                </Button>
              </Box>
            </Box>
          </Box>
        </Box>
      </Box>
    </Box>
  );
}