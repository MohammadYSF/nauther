import { AppBar, Toolbar, Typography, Box, Avatar } from '@mui/material';

export default function Topbar() {
  return (
    <AppBar
      position="static"
      sx={{
        bgcolor: '#337ab7',
        boxShadow: 'none',
        justifyContent: 'center',
      }}
    >
      <Toolbar
        sx={{
          minHeight: 56,
          py: 1.5,
          px: 2,
          display: 'flex',
          flexDirection: 'row',
          justifyContent: 'space-between',
          direction: 'rtl', // <-- Only this line changed
        }}
      >
        {/* Right: DIMA logo */}
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          <Typography variant="body2" sx={{ color: '#fff', fontWeight: 700, fontSize: 18, letterSpacing: 2 }}>
            DIMA
          </Typography>
        </Box>
        {/* Left: User full name and avatar */}
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          <Typography variant="body2" sx={{ color: '#fff', fontWeight: 500, ml: 1 }}>
            سهند افشردی
          </Typography>
          <Avatar sx={{ width: 24, height: 24 }} />
        </Box>
      </Toolbar>
    </AppBar>
  );
}