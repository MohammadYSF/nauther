import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getAdmins } from '../services/adminService';
import {
  Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Avatar, IconButton, Box,
  TablePagination, TextField, InputAdornment, MenuItem, FormControl, Select, Checkbox, Button
} from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import RolePopover from './RolePopover';

const rolesList = ['سوپر ادمین', 'ادمین', 'کاربر عادی'];

export default function UserTable({selected, setSelected}: {selected: string[], setSelected: React.Dispatch<React.SetStateAction<string[]>>}) {
  const [users, setUsers] = useState<any[]>([]);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [search, setSearch] = useState('');
  const [loading, setLoading] = useState(true);
  const [popoverAnchor, setPopoverAnchor] = useState<HTMLElement | null>(null);
  const [roleFilter, setRoleFilter] = useState('');
  // const [selected, setSelected] = useState<string[]>([]);

  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    getAdmins(page + 1, rowsPerPage, search)
      .then(res => {
        setUsers(res.data.items || res.data);
        setTotal(res.data.total || 0);
      })
      .finally(() => setLoading(false));
  }, [page, rowsPerPage, search]);

  const handleSelectAll = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.checked) {
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

  const handleChangePage = (_: any, newPage: number) => setPage(newPage);
  const handleChangeRowsPerPage = (e: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(parseInt(e.target.value, 10));
    setPage(0);
  };

  const handleRoleClick = (event: React.MouseEvent<HTMLElement>) => {
    setPopoverAnchor(event.currentTarget);
  };

  const handlePopoverClose = () => {
    setPopoverAnchor(null);
  };

  return (
    <>
      <Box sx={{ display: 'flex', gap: 2, mb: 2, alignItems: 'center', flexWrap: 'wrap', direction: 'rtl' }}>
        <TextField
          size="small"
          placeholder="جستجو نام یا کد"
          value={search}
          onChange={e => setSearch(e.target.value)}
          sx={{ width: 200 }}
          InputProps={{
            startAdornment: (
              <InputAdornment position="start">
                <SearchIcon fontSize="small" />
              </InputAdornment>
            ),
          }}
        />
        <FormControl size="small" sx={{ minWidth: 140 }}>
          <Select
            displayEmpty
            value={roleFilter}
            onChange={e => setRoleFilter(e.target.value)}
          >
            <MenuItem value="">
              <em>همه نقش‌ها</em>
            </MenuItem>
            {rolesList.map((role, idx) => (
              <MenuItem key={idx} value={role}>{role}</MenuItem>
            ))}
          </Select>
        </FormControl>


      </Box>
      <TableContainer component={Paper} sx={{ boxShadow: 0, direction: 'rtl' }}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell padding="checkbox">
                <Checkbox
                  checked={users.length > 0 && selected.length === users.length}
                  indeterminate={selected.length > 0 && selected.length < users.length}
                  onChange={handleSelectAll}
                  inputProps={{ 'aria-label': 'select all users' }}
                />
              </TableCell>
              <TableCell align="right" sx={{ width: 120 }}>وضعیت</TableCell>
              <TableCell align="right">شناسه</TableCell>
              <TableCell align="right">نام و نام خانوادگی</TableCell>
              <TableCell align="right">نقش</TableCell>
              <TableCell align="right">آخرین لاگین</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {users.map((user, idx) => (
              <TableRow
                key={user.id}
                hover
                onDoubleClick={() => navigate(`/admin/edit/${user.id}`)}
                sx={{
                  position: 'relative',
                  backgroundColor: idx % 2 === 0 ? '#fafbfc' : '#fff',
                  cursor: 'pointer',
                }}
                selected={isSelected(user.id)}
              >
                <TableCell padding="checkbox">
                  <Checkbox
                    checked={isSelected(user.id)}
                    onChange={() => handleSelectRow(user.id)}
                  />
                </TableCell>
                <TableCell align="right">
                  <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'start', height: 48 }}>
                    <Box
                      sx={{
                        width: 4,
                        height: 34,
                        borderRadius: 2,
                        bgcolor: '#21C362',
                        mr: 0,
                      }}
                    />
                    <span dir="rtl" style={{ fontWeight: 500, marginRight: 20 }}>
                      {user.status}
                    </span>
                    </Box>
                  </TableCell>
                <TableCell align="right">{user.code}</TableCell>
                <TableCell align="right">
                  <Box sx={{ display: 'inline-flex', alignItems: 'center', justifyContent: 'end' }}>
                    <Avatar sx={{ ml: 1, width: 24, height: 24 }} />
                    {user.name}
                  </Box>
                </TableCell>
                <TableCell
                  align="right"
                  sx={{ cursor: 'pointer', color: '#337ab7', fontWeight: 500 }}
                  onClick={handleRoleClick}
                >
                  {user.role}
                  {idx === 1 && (
                    <Box
                      component="span"
                      sx={{
                        bgcolor: '#e3f2fd',
                        color: '#1976d2',
                        fontSize: 10,
                        borderRadius: 1,
                        px: 1,
                        ml: 1,
                        height: 20,
                        display: 'inline-flex',
                        alignItems: 'center',
                      }}
                    >
                      شما
                    </Box>
                  )}
                </TableCell>
                <TableCell align="right">{user.lastLogin}</TableCell>
              </TableRow>
            ))}
            {users.length === 0 && (
              <TableRow>
                <TableCell colSpan={7} align="center">
                  داده‌ای یافت نشد.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        component="div"
        count={total}
        page={page}
        onPageChange={handleChangePage}
        rowsPerPage={rowsPerPage}
        onRowsPerPageChange={handleChangeRowsPerPage}
        labelRowsPerPage="تعداد در صفحه"
        rowsPerPageOptions={[5, 10, 20, 50]}
        sx={{ direction: 'rtl' }}
      />
      <RolePopover
        anchorEl={popoverAnchor}
        open={Boolean(popoverAnchor)}
        onClose={handlePopoverClose}
      />
    </>
  );
}
