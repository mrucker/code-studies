o_cnt = 100;
p_cnt = 4; %plotting only works for 1 & 2

X = rand([o_cnt p_cnt]) * 10;
T = rand([p_cnt 1]) * 10;
Y = X * T + 5 + 6*randn(o_cnt, 1);

%batch regression method
t_batch = (X'*X)^(-1) * X' * Y;

t_recur = [];
B       = [];
%recursive regression method
for i = 1:o_cnt
    [B, t_recur] = recursive_linear_regression(B, t_recur, X(i,:), Y(i), 1);
end

if p_cnt == 1
    t_x = 0:.1:10; 
    plot(X,Y, 'o', t_x,t_x*t_batch, ':', t_x,t_x*t_recur, '--');
    legend('data','batch fit','recursive fit');
elseif p_cnt == 2
    t_x = 0:.1:10;
    t_x = vertcat(reshape(repmat(t_x,numel(t_x),1), [1,numel(t_x)^2]), reshape(repmat(t_x',1,numel(t_x)), [1,numel(t_x)^2]))';
    plot3(X(:,1),X(:,2),Y, 'o', t_x(:,1),t_x(:,2),t_x*t_batch, ':', t_x(:,1),t_x(:,2),t_x*t_recur, '--');
    legend('data','batch fit','recursive fit');    
end

header = {'TRUTH', 'BATCH', 'RECUR'};
values = [T, t_batch, t_recur];

fprintf('\n')
fprintf('%7s %7s %7s\n', header{:})
fprintf('%07.4f %07.4f %07.4f\n', values)

function [B, theta] = recursive_linear_regression(B, theta, x, y, l)
        
    if(~any(theta) || all(theta == theta(1)))
        %see page 351 in ADP for why ".1" (aka, B0 = I * ["a small constant"])
        %this never seemed to give me good approximations no matter how many observations I fed in 
        %there must have been a bug in my calculations because this now works very well
        B = eye(size(x,2)) * .01;
        
        if(isempty(theta))
            theta = zeros(size(x,2),1);
        end

%        for now commenting the full inverse method out since the above
%        seems to work well now that I've fixed a few calculation errors.
%         B = [B;x,y];
%         X = B(:,1:end-1);
%         Y = B(:,end);
% 
%         if isInvertible(X'*X)
%             %the first time we do a batch update
%             %after this we only do recursive updates
%             B     = (X'*X)^(-1);
%             theta = B * X' * Y;
%             return;
%         else
%             %not enough observations yet
%             %to do our initial batch update
%             return;
%         end
    end

    assert(iscolumn(theta) && isnumeric(theta), 'theta must be a numeric column vector')
    assert(isrow(x)        && isnumeric(x)    , 'x must be a numeric row vector')
    assert(isscalar(y)     && isnumeric(y)    , 'y must be a numeric scalar')
    assert(size(theta,1) == size(x,2)         , 'theta and x must be equal dimension');

    x = x'; %all the equations below assume x and y are column vectors;

    e     = theta' * x - y;
    g     = l + x'*B*x;
    H     = 1/g * B;

    %update steps
    theta = theta - H * x * e;
    B     = 1/l*(B - 1/g * (B * (x * x') * B));

end