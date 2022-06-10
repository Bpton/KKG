$.fn.duplicate = function(count, cloneEvents, callback) {
    var stack = [], el;
    while(count--) { 
        el = this.clone( cloneEvents );
        callback && callback.call(el);
        stack.push( el.get()[0] );
    }
    return this.pushStack( stack );
};