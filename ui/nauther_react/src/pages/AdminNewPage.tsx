import {
  Box,
  Typography,
  TextField,
  InputAdornment,
  IconButton,
  Avatar,
  Button,
  MenuItem,
  FormControl,
  Chip,
  OutlinedInput,
  Paper,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  Popper,
  ClickAwayListener,
  Select,
} from '@mui/material';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import SearchIcon from '@mui/icons-material/Search';
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
import Sidebar from '../components/Sidebar';
import Topbar from '../components/Topbar';
import { useEffect, useState, useRef } from 'react';
import { useParams } from 'react-router-dom';
import { getAdminById, getAdmins } from '../services/adminService';
import { getRoles } from '../services/roleService';

export default function AdminNewPage() {
  const { id } = useParams();
  const isEdit = Boolean(id);

  // Fetch all users for selection
  const [users, setUsers] = useState<any[]>([]);
  // Form state
  const [selectedUser, setSelectedUser] = useState<any>(null);
  const [permissions, setPermissions] = useState<{ id: string, name: string, displayName: string }[]>([]);
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
        setSelectedUser(res); // Adjust according to your API response
        setPermissions(res.permissions);
        setPassword(''); // Usually left blank for security
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
    <Box sx={{ bgcolor: '#f4f6f8', minHeight: '100vh', width: '100vw', p: 0, m: 0 }}>
      <Box sx={{ width: '100vw', minHeight: '100vh', position: 'relative' }}>
        <Sidebar />
        <Box sx={{ pr: 7 }}>
          <Topbar />
          <Box sx={{ p: 4, pt: 5, maxWidth: 900, mx: 'auto', display: 'flex', flexDirection: 'column', alignItems: 'flex-end' }}>
            <Typography variant="h6" sx={{ fontWeight: 700, mb: 3 }}>
              <span style={{ color: '#337ab7', fontWeight: 700 }}>
                {isEdit ? `ویرایش ادمین ${id}` : 'ادمین جدید'}
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
              {/* Right column: user select and permissions */}
              <Box sx={{ minWidth: 320, flex: 1 }}>
                <Typography sx={{ mb: 1, fontWeight: 500 }}>نام کاربر</Typography>
                <Box ref={anchorRef} sx={{ position: 'relative', mb: 2 }}>
                  <Paper
                    sx={{
                      p: 0,
                      display: 'flex',
                      alignItems: 'center',
                      borderRadius: 2,
                      boxShadow: userDropdown ? 4 : 1,
                      cursor: isEdit ? 'not-allowed' : 'pointer',
                      border: userDropdown ? '1.5px solid #337ab7' : '1px solid #e0e0e0',
                      minHeight: 44,
                      bgcolor: isEdit ? '#f5f5f5' : 'inherit',
                    }}
                    onClick={() => {
                      if (!isEdit) setUserDropdown((prev) => !prev);
                    }}
                  >
                    <Box sx={{ flex: 1, px: 2, py: 1, display: 'flex', alignItems: 'center' }}>
                      {selectedUser ? (
                        <>
                          <Typography sx={{ fontWeight: 500, ml: 1 }}>{selectedUser.name}</Typography>
                          <Avatar sx={{ width: 24, height: 24, ml: 1 }} src={selectedUser.avatar} />
                          <Typography sx={{ color: '#888', fontSize: 13 }}>{selectedUser.id}</Typography>
                        </>
                      ) : (
                        <Typography sx={{ color: '#aaa' }}>انتخاب کنید</Typography>
                      )}
                    </Box>
                    <ArrowDropDownIcon sx={{ color: '#888', mr: 1 }} />
                  </Paper>
                  {!isEdit && (
                    <Popper
                      open={userDropdown}
                      anchorEl={anchorRef.current}
                      placement="bottom-end"
                      sx={{ zIndex: 1301, width: anchorRef.current?.offsetWidth || 320 }}
                    >
                      <ClickAwayListener onClickAway={() => setUserDropdown(false)}>
                        <Paper sx={{ mt: 1, borderRadius: 3, p: 1, minWidth: 320, maxWidth: 350 }}>
                          <Box sx={{ px: 1, pb: 1 }}>
                            <TextField
                              fullWidth
                              size="small"
                              placeholder="جستجو"
                              value={userSearch}
                              onChange={e => setUserSearch(e.target.value)}
                              InputProps={{
                                startAdornment: (
                                  <InputAdornment position="start">
                                    <SearchIcon fontSize="small" />
                                  </InputAdornment>
                                ),
                              }}
                            />
                          </Box>
                          <List dense sx={{ maxHeight: 220, overflowY: 'auto', direction: 'rtl' }}>
                            {filteredUsers.map((user) => (
                              <ListItem
                                key={user.id}
                                onClick={() => {
                                  setSelectedUser(user);
                                  setUserDropdown(false);
                                }}
                                sx={{
                                  borderRadius: 2,
                                  mb: 0.5,
                                  '&:hover': { bgcolor: '#f0f7ff' },
                                }}
                              >
                                <Typography sx={{ color: '#888', fontSize: 13, minWidth: 60 }}>{user.id}</Typography>
                                <ListItemAvatar>
                                  <Avatar sx={{ width: 24, height: 24 }} src={user.avatar} />
                                </ListItemAvatar>
                                <ListItemText
                                  primary={user.name}
                                  primaryTypographyProps={{ sx: { fontWeight: 500, fontSize: 15 } }}
                                  sx={{ textAlign: 'right', mr: 1 }}
                                />
                              </ListItem>
                            ))}
                          </List>
                        </Paper>
                      </ClickAwayListener>
                    </Popper>
                  )}
                </Box>
                <Typography sx={{ mb: 1, fontWeight: 500 }}>دسترسی‌ها</Typography>
                <FormControl fullWidth size="small" sx={{ mb: 2 }}>
                  <Select
                    multiple
                    displayEmpty
                    value={permissions}
                    onChange={e => {
                      let x = e.target.value;
                      setPermissions(x as { id: string, name: string, displayName: string }[]);

                    }
                    }
                    input={<OutlinedInput />}
                    renderValue={selected =>
                      (selected).length === 0
                        ? <em>انتخاب کنید</em>
                        : (
                          <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
                            {(selected).map(item => (
                              <Chip key={item.id} label={item.displayName} />
                            ))}
                          </Box>
                        )
                    }
                  >
                    <MenuItem disabled value="">
                      <em>انتخاب کنید</em>
                    </MenuItem>
                    {roles.map((r: any) => (
                      <MenuItem key={r.id} value={r.id}>{r.name}</MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Box>
              {/* Left column: password */}
              <Box sx={{ minWidth: 320, flex: 2 }}>
                <Typography sx={{ mb: 1, fontWeight: 500 }}>رمز عبور</Typography>
                <TextField
                  fullWidth
                  size="small"
                  type="password"
                  value={password}
                  onChange={e => setPassword(e.target.value)}
                  sx={{ mb: 2 }}
                  InputProps={{
                    endAdornment: (
                      <InputAdornment position="end">
                        <IconButton tabIndex={-1} edge="end" size="small" disabled>
                          <VisibilityOffIcon />
                        </IconButton>
                      </InputAdornment>
                    ),
                  }}
                />
                <Typography sx={{ mb: 1, fontWeight: 500 }}>تکرار رمز عبور</Typography>
                <TextField
                  fullWidth
                  size="small"
                  type="password"
                  value={confirmPassword}
                  onChange={e => setConfirmPassword(e.target.value)}
                  sx={{ mb: 2 }}
                />
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