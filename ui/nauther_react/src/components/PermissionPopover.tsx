import { Popover, Box, Typography, Avatar, IconButton } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

interface RolePopoverProps {
  anchorEl: HTMLElement | null;
  onClose: () => void;
  open: boolean;
}

const admins = [
    'ساخت کاربر',
    'ساخت دوربین',
    'ساخت بلاک'  

];

export default function PermissionPopover({ anchorEl, onClose, open }: RolePopoverProps) {
  return (
    <Popover
      open={open}
      anchorEl={anchorEl}
      onClose={onClose}
      anchorOrigin={{ vertical: 'center', horizontal: 'center' }}
      transformOrigin={{ vertical: 'center', horizontal: 'center' }}
      PaperProps={{
        sx: { p: 2, borderRadius: 3, minWidth: 320, maxWidth: 350, direction: 'rtl' },
      }}
      BackdropProps={{
        sx: { backgroundColor: 'rgba(0,0,0,0.2)' },
      }}
    >
      <Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
        <Typography sx={{ fontWeight: 500, flex: 1 }}>سهیل کیانی</Typography>
        <Avatar sx={{ width: 24, height: 24, ml: 1 }} />
        <IconButton size="small" onClick={onClose} sx={{ ml: 'auto' }}>
          <CloseIcon fontSize="small" />
        </IconButton>
      </Box>
      <ul style={{ paddingRight: 18, margin: 0, direction: 'rtl' }}>
        {admins.map((admin, idx) => (
          <li key={idx} style={{ fontSize: 15, marginBottom: 8, wordBreak: 'break-word' }}>
            {admin}
          </li>
        ))}
      </ul>
    </Popover>
  );
}