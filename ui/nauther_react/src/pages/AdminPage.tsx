import { Box, Button, Typography, Paper } from '@mui/material';
import Sidebar from '../components/Sidebar';
import Topbar from '../components/Topbar';
import UserTable from '../components/UserTable';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';

export default function AdminPage() {
  const navigate = useNavigate();
  const [selected, setSelected] = useState<string[]>([]);
  const handleDeleteSelected = () => {
    // Implement delete logic here if needed
    setSelected([]);
  };
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
              <Typography variant="h6" sx={{ fontWeight: 700 }}>ادمین</Typography>
            <Box sx={{ display: 'flex', justifyContent: 'start', alignItems: 'center', mb: 2 }}>
              <Button
                variant="contained"
                color="primary"
                sx={{ borderRadius: 2 }}
                onClick={() => navigate('/admin/new')}
              >
                ادمین جدید
              </Button>
                      <Button
                      sx={{marginRight: 1}}
          variant="contained"
          color="error"
          disabled={selected.length === 0}
          onClick={handleDeleteSelected}
        >
          حذف انتخاب شده‌ها
        </Button>
            </Box>
            <UserTable selected={selected} setSelected={setSelected} />
          </Paper>
        </Box>
      </Box>
    </Box>
  );
}