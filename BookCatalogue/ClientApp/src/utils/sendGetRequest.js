import axios from 'axios';

export const sendGetRequest = async (endpoint) => {
  let res = await axios({
    url: endpoint,
    method: 'get',
    headers: {
      'Content-Type': 'application/json'
    }
  });
  if (res.status === 200) {
    console.log(res.status);
  }
  return res.data;
};
