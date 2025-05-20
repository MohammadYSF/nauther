import { Box, IconButton, Tooltip } from '@mui/material';
import PeopleIcon from '@mui/icons-material/People';
import CameraAltIcon from '@mui/icons-material/CameraAlt';
import SettingsIcon from '@mui/icons-material/Settings';
import SearchIcon from '@mui/icons-material/Search';
import FilterListIcon from '@mui/icons-material/FilterList';
import MoreVertIcon from '@mui/icons-material/MoreVert';

const menu = [
  { icon: <PeopleIcon />, label: 'ادمین' },
  { icon: <CameraAltIcon />, label: 'دوربین' },
  { icon: <SearchIcon />, label: 'جستجو' },
  { icon: <FilterListIcon />, label: 'فیلتر' },
  { icon: <MoreVertIcon />, label: 'بیشتر' },
  { icon: <SettingsIcon />, label: 'تنظیمات' },
];

export default function Sidebar() {
  return (
    <Box
      sx={{
        width: 56,
        bgcolor: '#337ab7',
        height: '100%',
        position: 'absolute',
        right: 0,
        top: 0,
        borderRadius: '0 0 8px 0',
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        pt: 6,
        zIndex: 2,
      }}
    >
      {menu.map((item, idx) => (
        <Tooltip title={item.label} placement="left" key={idx}>
          <IconButton sx={{ color: '#fff', mb: 2 }}>{item.icon}</IconButton>
        </Tooltip>
      ))}
    </Box>
  );
}