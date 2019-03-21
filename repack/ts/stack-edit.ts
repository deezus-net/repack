import * as axios from 'axios';

class StackEdit{
    constructor(){
        this.init();
    }
    
    private init = () => {
        const btn = document.getElementById('get-token');
        if (btn) {
            btn.addEventListener('click', async (e) => {
                const input = document.getElementById('token');
                if(input){
                    const token = await axios.default.get<string>('./token');
                    input.setAttribute('value', token.data);
                }
            });
        }
    }
}
window.onload = () => {
    new StackEdit();
};