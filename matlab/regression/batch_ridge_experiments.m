run(fullfile(fileparts(which(mfilename)), '..', 'paths.m'))

lambda = 1;

os = 10;
ps = 1;

f = @(xs) xs.^3 + 500*1./exp(((-xs+3)/2).^2) + 300*rand(1,os);

xs_train = round(rand(ps,os) * 11 - .5);
ys_train = f(xs_train);

xs_test = rand(ps,os) * 10;
ys_test = f(xs_test);

%dk = @(xi)       (xs*xs' + eye(ps)*lambda)^-1 * xs*ys'    ; %traditional solution
%dk = @(xi) xs  * (xs'*xs + eye(os)*lambda)^-1 * ys'       ; %transpose'd solution
dk_1 = @(xi) ys_train  * (K(xs_train,xs_train,1) + eye(os)*lambda)^-1 * K(xs_train,xi,1); %linear
dk_2 = @(xi) ys_train  * (K(xs_train,xs_train,2) + eye(os)*lambda)^-1 * K(xs_train,xi,2); %polynomial
dk_4 = @(xi) ys_train  * (K(xs_train,xs_train,4) + eye(os)*lambda)^-1 * K(xs_train,xi,4); %equality
dk_5 = @(xi) ys_train  * (K(xs_train,xs_train,5) + eye(os)*lambda)^-1 * K(xs_train,xi,5); %gaussian
dk_6 = @(xi) ys_train  * (K(xs_train,xs_train,6) + eye(os)*lambda)^-1 * K(xs_train,xi,6); %exponential

fprintf('\nlinear MSE = %.2f'     , mean((ys_test - dk_1(xs_test)).^2));
fprintf('\npoly MSE = %.2f'       , mean((ys_test - dk_2(xs_test)).^2));
fprintf('\nequal MSE = %.2f'      , mean((ys_test - dk_4(xs_test)).^2));
fprintf('\ngaussian MSE =  %.2f'  , mean((ys_test - dk_5(xs_test)).^2));
fprintf('\nexponential MSE = %.2f', mean((ys_test - dk_6(xs_test)).^2));
fprintf('\n');

draw(xs_train,ys_train,dk_5,500);

%Drawers
function draw (xs, ys, dk, steps)
    max_x = max(xs);
    min_x = min(xs);

    rx = (min_x:(max_x-min_x)/steps:max_x);
    ry = dk(rx);

    clf
    hold on
    %axis([min_x max_x min_y max_y]);

    scatter(xs,ys, [], 'r', 'o');
    scatter(rx,ry, [], 'g', '.');

    hold off
end
%Drawers

%Kernels
function k = K(x1, x2, k_i)
    p = 10.0;
    c = 2.0;
    s = 5;
    n = size(x1,1);

    switch k_i
        case 1
            b = k_dot();
        case 2
            b = k_polynomial(k_dot(),p,c);
        case 3
            b = k_hamming(0);
        case 4
            b = k_equal(k_norm());
        case 5
            b = k_gaussian(k_norm(),s);
        case 6
            b = k_exponential(k_norm(),s);
        case 7
            b = k_anova(n);
        case 8
            b = k_exponential_compact(k_norm(),s);
    end

    k = b(x1,x2);
end