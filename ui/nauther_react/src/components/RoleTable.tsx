import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getRoles } from '../services/roleService';
import {
  Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Avatar, IconButton, Box,
  TablePagination, TextField, InputAdornment, MenuItem, FormControl, Select, TableSortLabel
} from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import AdminPopover from './AdminPopover';
import PermissionPopover from './PermissionPopover';



export default function RoleTable() {
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
        setRoles(res.data.items || res.data); // Adjust according to your API response
        setTotal(res.data.total || 0);
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

  // Filter and search logic
  const filteredUsers = roles.filter(
    r =>
      (r.name.includes(search))
  );


  const handleChangePage = (_: unknown, newPage: number) => setPage(newPage);
  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  return (
    <>
      <Box sx={{ display: 'flex', gap: 2, mb: 2, alignItems: 'center', flexWrap: 'wrap', direction: 'rtl' }}>
        <TextField
          size="small"
          placeholder="جستجو نقش"
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
      </Box>
      <TableContainer component={Paper} sx={{ boxShadow: 0, direction: 'rtl' }}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell align="right" >

                  نقش
              </TableCell>
              <TableCell align="right" >


                    دسترسی ها

              </TableCell>
              <TableCell align="right">

                  ادمین های نقش
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {roles.map((role, idx) => (
              <TableRow
                key={role.id}
                hover
                onDoubleClick={() => navigate(`/role/edit/${role.id}`)}
                sx={{
                  position: 'relative',
                  backgroundColor: idx % 2 === 0 ? '#fafbfc' : '#fff',
                  cursor: 'pointer',
                }}
              >
                <TableCell align="right">{role.name}</TableCell>
                <TableCell
                  align="right"
                  sx={{ cursor: 'pointer', color: '#337ab7', fontWeight: 500 }}
                  onClick={handlePermissionClick}
                >
                  {role.permissions[0]??""}
                  {1 && (
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
                      + {role.permissions.length - 1}
                    </Box>
                  )}
                </TableCell>
                <TableCell
                  align="right"
                  sx={{ cursor: 'pointer', color: '#337ab7', fontWeight: 500 }}
                  onClick={handleAdminClick}
                >
                  {role.admins[0]??""}
                  {1 && (
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
                      + {role.admins.length - 1}
                    </Box>
                  )}
                </TableCell>
              </TableRow>
            ))}
            {roles.length === 0 && (
              <TableRow>
                <TableCell colSpan={6} align="center">
                  داده‌ای یافت نشد.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        component="div"
        count={filteredUsers.length}
        page={page}
        onPageChange={handleChangePage}
        rowsPerPage={rowsPerPage}
        onRowsPerPageChange={handleChangeRowsPerPage}
        labelRowsPerPage="تعداد در صفحه"
        rowsPerPageOptions={[5, 10, 20, 50]}
        sx={{ direction: 'rtl' }}
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
