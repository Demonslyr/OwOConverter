import http from 'k6/http';
import { check, group, sleep, fail } from 'k6';

export let options = {
  vus: 1,  // 1 user looping for 1 minute
  duration: '1m',

  thresholds: {
    'http_req_duration': ['p(95)<1500'], // 95% of requests must complete below 1.5s
  }
};

const BASE_URL = 'http://HAProxyClusterLB.drinkpoint.me';

export default () => {
  let headerCollection = { headers: {
    Host: `uwu-converter.uwu-converter.atriarch.systems`
  }};

  let response = http.get(`${BASE_URL}/test string`, headerCollection);
  check(response.body, { 'is result owo converted': (body) => body === 'test stwing' });

  sleep(1);
}
