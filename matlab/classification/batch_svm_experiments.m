run(fullfile(fileparts(which(mfilename)), '..', 'paths.m'))

%Y_Linearly_Seperable_Y_Centered();
%N_Linearly_Seperable_Y_Centered();
%Y_Linearly_Seperable_N_Centered();
tic;
N_Linearly_Seperable_N_Centered();
toc;

function Y_Linearly_Seperable_Y_Centered()
    %not linearly separable, centered at 0;
    x = [-1 -1; 1 1; -1 1; 1 -1]';
    y = [1 -1 1 -1]';

    Tests(x,y);
end

function N_Linearly_Seperable_Y_Centered()
    %not linearly separable, centered at 0;
    x = [-1 -1; 1 1; -1 1; 1 -1]';
    y = [1 1 -1 -1]';
    
    Tests(x,y);
end

function Y_Linearly_Seperable_N_Centered()
    %not linearly separable, centered at 0;
    
    y = [1 -1 1 -1]';

    %shifting causes several incorrect classifications because the four points are seperable horizontally
    x = [-1 -1; 1 1; -1 1; 1 -1]';
    x = x + repmat(200,2,4);
    Tests(x,y);
    
    %rotating so the separation is now diagonal allows for correct classification
    x = [-1 -1; 1 1; -1 1; 1 -1]';
    x = [cos(7/4*pi) cos(1/4*pi); sin(7/4*pi) sin(1/4*pi)]*x;
    x = x + repmat(200,2,4);
    Tests(x,y);
    
end

function N_Linearly_Seperable_N_Centered()
    
    count = 100;
    center_1 = [0;5];
    center_2 = [.5;5.5];
    d_1 = 1;
    d_2 = 1;

    x1 = rand(2,count)*d_1 - [d_1/2; d_1/2];
    x2 = rand(2,count)*d_2 - [d_2/2; d_2/2];
    
    x1 = x1 + center_1;
    x2 = x2 + center_2;
    
    y1 = ones(1,count);
    y2 = -ones(1,count);
    
    x = horzcat(x1,x2);
    y = horzcat(y1,y2)';
    
    [margin(1), right(1), wrong(1), unknown, dk] = maxMarginOptimization_4_s(y,x,k_gaussian(k_norm(),.01));
    
    draw(y,x,50,dk);
    
    horzcat(right',wrong')    
end

function Tests(x,y)
    [margin, right, wrong, unknown] = maxMarginOptimization_1_h(y,x);
    [margin, right, wrong, unknown] = maxMarginOptimization_1_s(y,x);

    [margin, right, wrong, unknown] = maxMarginOptimization_2_h(y,x);
    [margin, right, wrong, unknown] = maxMarginOptimization_2_s(y,x);

    [margin, right, wrong, unknown] = maxMarginOptimization_3_s(y,x);    
    
    [margin, right, wrong, unknown] = maxMarginOptimization_4_s(y,x, k_polynomial(k_dot(),1,1));
    [margin, right, wrong, unknown] = maxMarginOptimization_4_s(y,x, k_polynomial(k_dot(),2,1));
    [margin, right, wrong, unknown] = maxMarginOptimization_4_s(y,x, k_polynomial(k_dot(),3,1));
    [margin, right, wrong, unknown] = maxMarginOptimization_4_s(y,x, k_polynomial(k_dot(),10,1));
end

%Optimizers
function [margin, right, wrong, unknown, dk] = maxMarginOptimization_1_h(y, x)
    f_cnt = size(x,1);
    o_cnt = size(x,2);
        
    warning('off','all')
    cvx_begin
        cvx_quiet(true);
        variables m w(f_cnt) b0;
        maximize(m);
        subject to
            1 >= norm(w);
            m <= y.*(x'*w);
    cvx_end
    warning('off','all')
    
    dk = @(x) (x'*w);
    ds = y.*(x'*w);

    margin  = m;
    right   = sum(sign(ds) == 1);
    wrong   = sum(sign(ds) == -1);
    unknown = sum(sign(ds) == 0);
end
function [margin, right, wrong, unknown, dk] = maxMarginOptimization_1_s(y, x)
    f_cnt = size(x,1);
    o_cnt = size(x,2);
        
    warning('off','all')
    cvx_begin
        cvx_quiet(true);
        variables m w(f_cnt) z(o_cnt);
        maximize(m - sum(z)); %in this case z can easily grow much faster than m causing a tug of war
        subject to
            1 >= w'*w;
            m <= y.*(x'*w) + z;
            0 <= z;
    cvx_end
    warning('off','all')
    
    dk = @(x) (x'*w);
    ds = y.*(x'*w);

    margin  = m;
    right   = sum(sign(ds) == 1);
    wrong   = sum(sign(ds) == -1);
    unknown = sum(sign(ds) == 0);
end
function [margin, right, wrong, unknown, dk] = maxMarginOptimization_2_h(y, x)
    f_cnt = size(x,1);
    o_cnt = size(x,2);
    
    warning('off','all')
    cvx_begin
        cvx_quiet(true);
        variables w(f_cnt);
        minimize(norm(w));
        subject to
            1 <= y.*(x'*w);
    cvx_end
    warning('off','all')

    dk = @(x) (x'*w);
    ds = y.*(x'*w);

    margin  = 1/norm(w);
    right   = sum(sign(ds) == 1);
    wrong   = sum(sign(ds) == -1);
    unknown = sum(sign(ds) == 0);
end
function [margin, right, wrong, unknown, dk] = maxMarginOptimization_2_s(y, x)
    f_cnt = size(x,1);
    o_cnt = size(x,2);
    
    warning('off','all')
    cvx_begin
        cvx_quiet(true);
        variables w(f_cnt) z(o_cnt);
        minimize(norm(w) + sum(z));
        subject to
            1 <= y.*(x'*w) + z;
            0 <= z;
    cvx_end
    warning('off','all')
    
    dk = @(x) (x'*w);
    ds = y.*(x'*w);

    margin  = 1/norm(w);
    right   = sum(sign(ds) == 1);
    wrong   = sum(sign(ds) == -1);
    unknown = sum(sign(ds) == 0);
end
function [margin, right, wrong, unknown, dk] = maxMarginOptimization_3_s(y, x)
    f_cnt = size(x,1);
    o_cnt = size(x,2);

    warning('off','all')
    cvx_begin
        cvx_quiet(true);
        variables w(f_cnt);
        minimize( sum(max(0,1-y.*x'*w)) + norm(w)) %hing-loss
    cvx_end
    warning('off','all')        

    dk = @(x) (x'*w);
    ds = y.*(x'*w);

    margin  = 1/norm(w);
    right   = sum(sign(ds) == 1);
    wrong   = sum(sign(ds) == -1);
    unknown = sum(sign(ds) == 0);
end
function [margin, right, wrong, unknown, dk] = maxMarginOptimization_4_s(y, x, k)
    f_cnt = size(x,1);
    o_cnt = size(x,2);

    warning('off','all')
    cvx_begin
        cvx_quiet(true);
        variables a(o_cnt);
        maximize(sum(a) - 1/2*quad_form(a.*y, k(x,x))) %dual problem
        subject to
            0 == a'*y;
            0 <= a;
    cvx_end
    warning('off','all')
    
    %Useful to study kernel polynomials of degree 2. This is a direct calculation of the feature space we are working in.
    %f_x = f(x);
    
    %When working in higher dimensions I'm not sure this has any meaning. (such as polynomial kernels above 1).
    %When working within the dimensions of x this is the normal to the hyperplane.
    %f_w = f_x*(a.*y);
    %f_m = 1/norm(f_w);
    
    %regarding b0: "we typically use an average of all the solutions for numerical stability" (ESL pg.421)        
    sv = x(:,round(a,8)>0);
    sl = y(round(a,8)>0,1);
    b0 = mean(sl - k(sv, x)*(a.*y));
    
    dk = @(xk) k(xk,x)*(a.*y) + b0;
    ds = y.*dk(x);
        
    margin  = 1/sqrt(sum(a));
    right   = sum(sign(ds) == 1);
    wrong   = sum(sign(ds) == -1);
    unknown = sum(sign(ds) == 0);
end
%Optimizers

%Drawers
function draw (ls, xs, steps, dk)
    max_x = max(xs(1,:));
    min_x = min(xs(1,:));
    max_y = max(xs(2,:));
    min_y = min(xs(2,:));
    
    clf
    hold on
    axis([min_x max_x min_y max_y]);
    
    scatter(xs(1,ls==-1),xs(2,ls==-1), 'r')
    scatter(xs(1,ls==1),xs(2,ls==1), 'b')        
    
    for i = 1:steps+1
        xs = min_x:(max_x-min_x)/steps:max_x;
        ys = min_y:(max_y-min_y)/steps:max_y;
        
        boundary_p = [repmat(xs(i),1,steps+1); ys];                
        boundary_l = sign(dk(boundary_p));
        
        scatter(boundary_p(1,boundary_l==-1),boundary_p(2,boundary_l==-1), 'g', '.')
        scatter(boundary_p(1,boundary_l==1),boundary_p(2,boundary_l==1), 'y', '.')
    end
    
    hold off
end
%Drawers

%Kernels
function m = K(x1, x2)
    p = 1;
    c = 1;
    s = .1;
 
    b = k_polynomial(k_dot,p,c);
    
    m = b(x1,x2);
end
%Kernels

function features = f(x)

    features = [];
    
    for i = 1:size(x,2)
        n1 = [1];
        n2 = [sqrt(2)*(x(:,i))];
        n3 = [(x(:,i)).*(x(:,i))];
        
        n4=[];
        for j = 1:size(x,1)
            for k = (j+1):size(x,1)
                n4 = vertcat(n4,[sqrt(2)*x(j,i)*x(k,i)]);
            end
        end
        features = horzcat(features, vertcat(n1, n2, n3, n4));
    end        
end
