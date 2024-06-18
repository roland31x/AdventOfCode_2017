const url = 'https://adventofcode.com/';
import fs from 'fs';
import { get } from 'http';

async function fetch_input(day, year = 2017) {

    let session = fs.readFileSync('session.txt', 'utf8').trim();

    if(!session) {
        console.log('No session found in session.txt');
        return '';
    }

    const response = await fetch(url + year + '/day/' + day + '/input', {
        headers: {
            'Cookie': 'session=' + session,
            'User-Agent': 'roland31x/js-aoc-api v1.0'
        }
    });

    return await response.text();
}

export async function get_input(day, year = 2017) {
    let path = 'inputs/day' + day + '.txt';
    if(fs.existsSync(path)) {
        let input = fs.readFileSync(path).toString().split('\n');
        return input[input.length - 1] === '' ? input.slice(0, -1) : input
    }
    else{
        let resp = await fetch_input(day, year);
        fs.writeFileSync(path, resp);
        return await get_input(day, year);
    }
    
}