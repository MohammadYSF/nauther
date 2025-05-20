import { Box, Button, Typography, Paper } from '@mui/material';
import Sidebar from '../components/Sidebar';
import Topbar from '../components/Topbar';
import { useNavigate } from 'react-router-dom';
import RoleTable from '../components/RoleTable';
import { useEffect, useState } from 'react';
import { getRoles } from '../services/roleService';

export default function RolePage() {
  const navigate = useNavigate();
  const [roles, setRoles] = useState([] as any[]);

  useEffect(() => {
    getRoles().then(res => setRoles(res.data));
  }, []);

  return (
    <Box sx={{ bgcolor: '#f4f6f8', minHeight: '100vh', width: '100vw', p: 0, m: 0 }}>
      <Box
        sx={{
          borderRadius: 0,
          overflow: 'hidden',
          bgcolor: '#f4f6f8',
          width: '100vw',
          minHeight: '100vh',
          boxShadow: 0,
          position: 'relative',
        }}
      >
        <Sidebar />
        <Box sx={{ pr: 7 }}>
          <Topbar />
          <Paper
            sx={{
              mt: 3,
              p: 3,
              borderRadius: 2,
              boxShadow: 0,
              minHeight: 500,
              overflow: 'auto',
              width: 'calc(100vw - 100px)', // leave space for sidebar
              mx: 'auto',
              bgcolor: '#fff',
            }}
          >
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 2 }}>
              <Typography variant="h6" sx={{ fontWeight: 700 }}>نقش</Typography>
              <Button
                variant="contained"
                color="primary"
                sx={{ borderRadius: 2 }}
                onClick={() => navigate('/role/new')}
              >
                نقش جدید
              </Button>
            </Box>
            <RoleTable />
            <div>
              {roles.map(role => (
                <div key={role.id}>{role.name}</div>
              ))}
            </div>
          </Paper>
        </Box>
      </Box>
    </Box>
  );
}