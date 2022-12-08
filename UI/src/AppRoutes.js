import Counter from './components/Counter';
import FetchData from './components/FetchData';
import Home from './components/Home';

const AppRoutes = [
  {
    index: true,
    path: '/',
    element: <Home />,
    name: 'Home'
  },
  {
    path: '/counter',
    element: <Counter />,
    name: 'Counter'
  },
  {
    path: '/fetch-data',
    element: <FetchData />,
    name: 'Fetch data'
  }
];

export default AppRoutes;
